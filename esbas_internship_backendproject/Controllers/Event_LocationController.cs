using esbas_internship_backendproject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace esbas_internship_backendproject.Controllers
{
    [ApiController]
    [Route("api")]
    public class Event_LocationController : Controller
    {
        private readonly EsbasDbContext _context;

        public Event_LocationController(EsbasDbContext context)
        {
            _context = context;
        }


        [HttpGet("eventlocation")]
        [Produces("application/json")]

        public async Task<ActionResult<IEnumerable<Event_Location>>> GetEventLocation()
        {
            var event_location = await _context.Event_Location.ToListAsync();

            return Ok(event_location);
        }


        [HttpGet("eventlocation/{id}")]
        [Produces("application/json")]

        public async Task<ActionResult<IEnumerable<Event_Location>>> GetEvent_LocationById(int id)
        {

            var event_location = await _context.Event_Location.FindAsync(id);

            if(event_location == null)  return NotFound();

            return Ok(event_location);
        }

    
        [HttpPost("eventlocation")]
        [Produces("application/json")]

        public async Task<ActionResult<Event_Location>> PostEvent_Location([FromBody] Event_Location event_Location)
        {

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Event_Location ON");
                    _context.Event_Location.Add(event_Location);
                    await _context.SaveChangesAsync();
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Event_Location OFF");
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

            }
            return CreatedAtAction("GetEvent_Location", new { id = event_Location.L_ID }, event_Location);
        }

        [HttpPut("eventlocation/{id}")]
        [Produces("application/json")]

        public async Task<ActionResult> PutEvent_Location(int id, [FromBody] Event_Location event_Location)
        {

            if (id != event_Location.L_ID)
            {
                return BadRequest();
            }

            _context.Entry(event_Location).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Event_LocationExists(id))
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

        [HttpDelete("eventlocation/{id}")]
        [Produces("application/json")]

        public async Task<ActionResult> SoftDeleteEvent_Location(int id)
        {

            var eventlocation = await _context.Event_Location.FindAsync(id);

            if (eventlocation == null)
            {
                return NotFound();
            }

            eventlocation.Status = false;
            _context.Event_Location.Update(eventlocation);
            await _context.SaveChangesAsync();

            return NoContent();

        }
        private bool Event_LocationExists(int id)
        {
            return _context.Event_Location.Any(el => el.L_ID == id);
        }
    }

}
