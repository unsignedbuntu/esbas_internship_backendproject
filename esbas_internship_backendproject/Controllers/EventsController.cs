using esbas_internship_backendproject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace esbas_internship_backendproject.Controllers
{
    [ApiController]
    [Route("api")]
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
        [Produces("application/json")]
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

        [HttpGet("eventlocation")]
        [Produces("application/json")]

        public async Task<ActionResult<IEnumerable<Event_Location>>> GetEvent_Location()
        {

            var event_location = await _context.Event_Location.ToListAsync();

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
            return CreatedAtAction("GetEvent_Location", new { id = event_Location.Event_LocationID }, event_Location);
        }

        [HttpPut("eventlocation/{id}")]
        [Produces("application/json")]

        public async Task<ActionResult> PutEvent_Location(int id, [FromBody] Event_Location event_Location)
        {

            if (id != event_Location.Event_LocationID)
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


        [HttpGet("eventtype")]
        [Produces("application/json")]

        public async Task<ActionResult<IEnumerable<Event_Type>>> GetEvent_Type() {

            var event_type = await _context.Event_Type.ToListAsync();

            return Ok(event_type);
        }

        [HttpPost("eventtype")]
        [Produces("application/json")]

        public async Task<ActionResult<Event_Type>> PostEvent_Type([FromBody] Event_Type event_type)
        {

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Event_Type ON");
                    _context.Event_Type.Add(event_type);
                    await _context.SaveChangesAsync();
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Event_Type OFF");
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

            }
            return CreatedAtAction("GetEvent_Type", new { id = event_type.Event_TypeID }, event_type);
        }

        [HttpPut("eventtype/{id}")]
        [Produces("application/json")]

        public async Task<ActionResult>  PutEvent_Type(int id, [FromBody] Event_Type event_type){

            if (id != event_type.Event_TypeID)
            {
                return BadRequest();
            }

            _context.Entry(event_type).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Event_TypeExists(id))
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

        [HttpDelete("eventtype/{id}")]
        [Produces("application/json")]

        public async Task<ActionResult> SoftDeleteEvent_Type(int id)
        {

            var eventtype = await _context.Event_Type.FindAsync(id);

            if (eventtype == null)
            {
                return NotFound();
            }

            eventtype.Status = false;
            _context.Event_Type.Update(eventtype);
            await _context.SaveChangesAsync();

            return NoContent();

        }
        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventID == id);
        }
        private bool Event_LocationExists(int id)
        {
           return _context.Event_Location.Any(el => el.Event_LocationID == id); 
        }

        private bool Event_TypeExists(int id)
        {
            return _context.Event_Type.Any(et => et.Event_TypeID == id);
        }
    }
    
}
