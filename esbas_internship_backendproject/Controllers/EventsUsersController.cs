using esbas_internship_backendproject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace esbas_internship_backendproject.Controllers
{
    [ApiController]
    [Route("api")]
    public class EventsUsersController : Controller
    {
        private readonly EsbasDbContext _context;

        public EventsUsersController(EsbasDbContext context)
        {
            _context = context;
        }

        [HttpGet("eventsusers")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Events_Users>>> GetEvents_Users()
        {
 
            var events_Users = await _context.Events_Users
       .Include(eu => eu.Event)
       .Include(eu => eu.User)
       .ToListAsync();
           
           
            return Ok(events_Users);
        }

        [HttpPost("eventsusers")]
        [Produces("application/json")]

        public async Task<ActionResult<Events_Users>> PostEvents_Users([FromBody] Events_Users events_Users)
        {
            
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Events_Users ON");
                    _context.Events_Users.Add(events_Users);
                    await _context.SaveChangesAsync();
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Events_Users OFF");
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return CreatedAtAction("GetEvents_Users", new { id = events_Users.ID}, events_Users);
        }

        [HttpPut("eventsusers/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> PutEvents_Users(int id, [FromBody] Events_Users events_Users)

        {
            if (id != events_Users.ID)
            {
                return BadRequest();
            }

            _context.Entry(events_Users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventsUserExists(id))
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

        [HttpDelete("eventsusers/{eventId}/{userId}")]
        public async Task<IActionResult> SoftDeleteEvents_Users(int eventId, int userId)
        {
            var eventUser = await _context.Events_Users
                .FirstOrDefaultAsync(eu => eu.EventID == eventId && eu.UserID == userId);

            if (eventUser == null)
            {
                return NotFound();
            }

            eventUser.Status = false;
            _context.Events_Users.Update(eventUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool EventsUserExists(int id)
        {
            return _context.Events_Users.Any(eu => eu.ID == id);
        }
    }
}
