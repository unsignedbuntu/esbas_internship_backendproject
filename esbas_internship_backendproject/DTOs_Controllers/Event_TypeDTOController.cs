using esbas_internship_backendproject.DTOs;
using Microsoft.AspNetCore.Mvc;
using esbas_internship_backendproject.Entities;
using AutoMapper;

namespace esbas_internship_backendproject.DTOs_Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Event_TypeDTOController : ControllerBase
    {
        private readonly EsbasDbContext _context;
        private readonly IMapper _mapper;
        public Event_TypeDTOController(EsbasDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetEvents()
        {
            var eventTypes = _context.Event_Type
               .Select(et => _mapper.Map<EventTypeDTO>(et))
               .ToList();

            return Ok(eventTypes);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public IActionResult GetEventTypeByID(int id)
        {
            var eventType = _context.Event_Type
                .Where(et => et.T_ID == id)
                .Select(et => _mapper.Map<EventTypeDTO>(et))
                .FirstOrDefault();

            if (eventType == null)
            {
                return NotFound();
            }

            return Ok(eventType);
        }

        [HttpPost]
        [Produces("application/json")]
        public IActionResult CreateEventType([FromBody] EventTypeDTO eventTypeDTO)
        {
            if (eventTypeDTO == null)
            {
                return BadRequest();
            }

            var event_Type = new Event_Type
            {
                Name = eventTypeDTO.Name,
            };

            _context.Event_Type.Add(event_Type);
            _context.SaveChanges();

            return Ok(event_Type);
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        public IActionResult UpdateEventType(int id, [FromBody] EventTypeDTO eventTypeDTO)
        {
            if (eventTypeDTO == null)
            {
                return BadRequest();
            }

            var eventType = _context.Event_Type.FirstOrDefault(et => et.T_ID == id);

            if (eventType == null)
            {
                return NotFound();
            }

            eventType.Name = eventTypeDTO.Name;

            _context.SaveChanges();

            return Ok(eventType);
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        public IActionResult SoftDeleteEventType(int id)
        {
            var eventType = _context.Event_Type.FirstOrDefault(et => et.T_ID == id);

            if (eventType == null)
            {
                return NotFound();
            }

           
            eventType.Status = false;


            _context.Event_Type.Update(eventType);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
