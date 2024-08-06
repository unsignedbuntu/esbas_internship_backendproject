using esbas_internship_backendproject.DTOs;
using esbas_internship_backendproject.ResponseDTO;
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
        public IActionResult CreateEvents([FromBody] EventResponseDTO eventResponseDTO)
        {
            if (eventResponseDTO == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventsResponse = _mapper.Map<Events>(eventResponseDTO);

            _context.Events.Add(eventsResponse);
            _context.SaveChanges();

            return Ok(eventsResponse);
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        public IActionResult UpdateEvents(int id, [FromBody] EventResponseDTO eventResponseDTO )
        {
            if (eventResponseDTO == null)
            {
                return BadRequest();
            }

            var eventsResponse = _context.Events.FirstOrDefault(e => e.EventID == id);

            if (eventsResponse == null)
            {
                return NotFound();
            }

            eventsResponse.Name = eventResponseDTO.Name;
            eventsResponse.Type = eventResponseDTO.Type;
            eventsResponse.Location = eventResponseDTO.Location;
            eventsResponse.EventDateTime = eventResponseDTO.EventDateTime;

            _context.SaveChanges();

             return Ok(eventsResponse);
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

        /*[HttpDelete("ForceDelete/{id}")]
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
        }*/


    }
}
