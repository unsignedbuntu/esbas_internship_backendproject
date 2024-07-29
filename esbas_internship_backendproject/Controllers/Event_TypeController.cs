using esbas_internship_backendproject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace esbas_internship_backendproject.Controllers
{

        [ApiController]
        [Route("api")]
        public class Event_TypeController : Controller
        {
            private readonly EsbasDbContext _context;

            public Event_TypeController(EsbasDbContext context)
            {
                _context = context;
            }

        [HttpGet("eventtype")]
        [Produces("application/json")]

        public async Task<ActionResult<IEnumerable<Event_Type>>> GetEvent_Type()
        {

            var event_type = await _context.Event_Type.ToListAsync();

            return Ok(event_type);
        }

        [HttpGet("eventtype/{id}")]
        [Produces("application/json")]

        public async Task<ActionResult<IEnumerable<Event_Type>>> Get_Event_TypeById(int id)
        {
            var event_type = await _context.Event_Type.FindAsync(id);

            if (event_type == null) return NotFound();

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
            return CreatedAtAction("GetEvent_Type", new { id = event_type.T_ID }, event_type);
        }

        [HttpPut("eventtype/{id}")]
        [Produces("application/json")]

        public async Task<ActionResult> PutEvent_Type(int id, [FromBody] Event_Type event_type)
        {

            if (id != event_type.T_ID)
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

        private bool Event_TypeExists(int id)
        {
            return _context.Event_Type.Any(et => et.T_ID == id);
        }
    }
}
