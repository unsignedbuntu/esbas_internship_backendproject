using esbas_internship_backendproject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace esbas_internship_backendproject.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : Controller
    {
        private readonly EsbasDbContext _context;

        public EventsController(EsbasDbContext context)
        {
            _context = context;
        }
        [HttpGet("events")]
        [Produces("application/json")]

        public async Task<ActionResult<IEnumerable<Events>>> GetEvents()
        {
            var events = await _context.Events
            .Include(e => e.Users)
            .ThenInclude(eu => eu.User)
            .ToListAsync();

            return Ok(events);
        }

        [HttpPost("events")]
        [Produces("application/json")]

        public async Task<ActionResult<Events>> PostEvents([FromBody] Events events)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Events ON");
                    _context.Events.Add(events);
                    await _context.SaveChangesAsync();
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Events OFF");
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return CreatedAtAction("GetEvents", new { id = events.EventID }, events);
        }

        [HttpPut("events/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> PutEvents(int id, [FromBody] Events events)

        {
            if (id != events.EventID)
            {
                return BadRequest();
            }

            _context.Entry(events).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("event/{id}")]
        public async Task<IActionResult> SoftDeleteEvent(int id)
        {
            var eventEntity = await _context.Events.FindAsync(id);
            if (eventEntity == null)
            {
                return NotFound();
            }

            eventEntity.Status = false;
            _context.Events.Update(eventEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventID == id);
        }
    }
    
}
