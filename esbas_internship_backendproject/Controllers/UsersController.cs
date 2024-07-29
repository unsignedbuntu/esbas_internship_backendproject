using esbas_internship_backendproject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace esbas_internship_backendproject.Controllers
{
    [ApiController]
    [Route("api")]
    public class UsersController : ControllerBase
    {
        private readonly EsbasDbContext _context;

        public UsersController(EsbasDbContext context)
        {
            _context = context;
        }

        [HttpGet("users")]
        [Produces("application/json")]

        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            return Ok(users);
        }

        [HttpGet("users/{id}")]
        [Produces("application/json")]

        public async Task<ActionResult<IEnumerable<Users>>> GetUsersById(int id)
        {
            var users = await _context.Users.FindAsync(id);

            if(users == null) return NotFound();

            return Ok(users);
        }

        [HttpPost("users")]
        [Produces("application/json")]

        public async Task<ActionResult<Users>> PostUsers([FromBody] Users users)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Users ON");
                    _context.Users.Add(users);
                    await _context.SaveChangesAsync();
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Users OFF");
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            _context.Users.Add(users);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUsers", new { id = users.ID }, users);
        }

        [HttpPut("users/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> PutUser(int id, [FromBody] Users user)
        {
            if (id != user.ID)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        [HttpDelete("users/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> SoftDeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Status = false;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(u => u.ID == id);
        }

    }
}
