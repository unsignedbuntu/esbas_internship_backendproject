using esbas_internship_backendproject.DTOs;
using Microsoft.AspNetCore.Mvc;
using esbas_internship_backendproject.Entities;
using AutoMapper;

namespace esbas_internship_backendproject.DTOs_Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EventsDTOController : ControllerBase
    {
        private readonly EsbasDbContext _context;
        private readonly IMapper _mapper;
        public EventsDTOController(EsbasDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetEvents()
        {
            var events = _context.Events
                .Select(e => _mapper.Map<EventDTO>(e))
                .ToList();

            return Ok(events);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public IActionResult GetEventByID(int id)
        {
            var events = _context.Events
                .Where(e => e.EventID == id)
                .Select(e => _mapper.Map<EventDTO>(e))
                .FirstOrDefault();

            if (events == null)
            {
                return NotFound();
            }

            return Ok(events);
        }

        [HttpPost]
        [Produces("application/json")]
        public IActionResult CreateEvents([FromBody] EventDTO eventDTO)
        {
            if (eventDTO == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var events = new Events
            {
                Name = eventDTO.Name,
                Type = eventDTO.Type,
                Location = eventDTO.Location,
                EventDateTime = eventDTO.EventDateTime,
                
            };

            _context.Events.Add(events);
            _context.SaveChanges();

            return Ok(events);
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        public IActionResult UpdateEvents(int id, [FromBody] EventDTO eventsDTO )
        {
            if (eventsDTO == null)
            {
                return BadRequest();
            }

            var events = _context.Events.FirstOrDefault(e => e.EventID == id);

            if (events == null)
            {
                return NotFound();
            }

            events.Name = eventsDTO.Name;
            events.Type = eventsDTO.Type;
            events.Location = eventsDTO.Location;
            events.EventDateTime = eventsDTO.EventDateTime;

            _context.SaveChanges();

             return Ok(events);
        }

        [HttpDelete("SoftDelete{id}")]
        [Produces("application/json")]
        public IActionResult SoftDeleteEvents(int id)
        {
            var events = _context.Events.FirstOrDefault(e => e.EventID == id);

            if (events == null)
            {
                return NotFound();
            }


            events.Status = false;


            _context.Events.Update(events);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("ForceDelete/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);

            if (eventItem == null)
            {
                return NotFound();
            }

            _context.Events.Remove(eventItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
