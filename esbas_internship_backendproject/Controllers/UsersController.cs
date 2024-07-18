using esbas_internship_backendproject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace esbas_internship_backendproject.Controllers
{
    [ApiController]
    [Route("api/users")]
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
            var users = await _context.Users
            .Include(u => u.Events)
            .ThenInclude(eu => eu.Event)
            .ToListAsync();

            return Ok(users);
        }

        [HttpPost("users")]
        [Produces("application/json")]

        public async Task<ActionResult<Users>> PostUser([FromBody] Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsers), new { id = user.UserID }, user);
        }

        [HttpPut("users/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> PutUser(int id, [FromBody] Users user)
        {
            if (id != user.UserID)
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
                if (UserExists(id))
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

        [HttpDelete("user/{id}")]
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
            return _context.Users.Any(u => u.UserID == id);
        }

    }
}
