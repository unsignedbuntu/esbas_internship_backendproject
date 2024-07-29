using esbas_internship_backendproject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace esbas_internship_backendproject.Controllers
{
    [ApiController]
    [Route("api")]
    public class User_GenderController : Controller
    {
        private readonly EsbasDbContext _context;

        public User_GenderController(EsbasDbContext context)
        {
            _context = context;
        }

        [HttpGet("usergender/{id}")]
        [Produces("application/json")]

        public async Task<ActionResult<IEnumerable<User_Gender>>> Get_User_GenderById(int id)
        {
            var user_gender = await _context.User_Gender.FindAsync(id);

            if (user_gender == null) return NotFound();

            return Ok(user_gender);
        }

        [HttpGet("usergender")]
        [Produces("application/json")]

        public async Task<ActionResult<IEnumerable<User_Gender>>> GetUser_Gender()
        {
            var user_gender = await _context.User_Gender
            .ToListAsync();

            return Ok(user_gender);
        }

        [HttpPost("usergender")]
        [Produces("application/json")]

        public async Task<ActionResult> PostUser_Gender([FromBody] User_Gender user_Gender)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT User_Gender ON");
                    _context.User_Gender.Add(user_Gender);
                    await _context.SaveChangesAsync();
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT User_Gender OFF");
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            _context.User_Gender.Add(user_Gender);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser_Gender), new { id = user_Gender.G_ID }, user_Gender);

        }

        [HttpPut("usergender/{id}")]
        [Produces("application/json")]

        public async Task<ActionResult> Put_UserGender(int id, [FromBody] User_Gender user_Gender)
        {

            if (id != user_Gender.G_ID)
            {
                return BadRequest();
            }

            _context.Entry(user_Gender).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!User_GenderExists(id))
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

        [HttpDelete("usergender/{id}")]
        [Produces("application/json")]

        public async Task<ActionResult> SoftDeleteUser_Gender(int id)
        {
            var usergender = await _context.User_Gender.FindAsync(id);

            if (usergender == null)
            {
                return NotFound();
            }

            usergender.Status = false;
            _context.User_Gender.Update(usergender);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool User_GenderExists(int id)
        {
            return _context.User_Gender.Any(ug => ug.G_ID == id);
        }

    }
}
