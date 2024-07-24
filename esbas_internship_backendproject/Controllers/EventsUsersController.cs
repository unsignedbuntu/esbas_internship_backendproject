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
        public async Task<ActionResult<IEnumerable<Events_Users>>> GetEventsUsers()
        {
            /*var query = from eu in _context.Events_Users
                        join e in _context.Events on eu.EventID equals e.EventID
                        join u in _context.Users on eu.UserID equals u.UserID
                        select new Events_Users
                        {
                            Events_UserID = eu.Events_UserID,
                            UserID = u.UserID,
                            EventID = e.EventID,
                        };
           */ 
            var eventsUsers = await _context.Events_Users
       .Include(eu => eu.Event)
       .Include(eu => eu.User)
       .ToListAsync();
           
           
            return Ok(eventsUsers);
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
            return CreatedAtAction("GetEvents_Users", new { id = events_Users.Events_UserID }, events_Users);
        }

        [HttpPut("eventsusers/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> PutEvents_Users(int id, [FromBody] Events_Users events_Users)

        {
            if (id != events_Users.Events_UserID)
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
            return _context.Events_Users.Any(eu => eu.Events_UserID == id);
        }
    }
}
