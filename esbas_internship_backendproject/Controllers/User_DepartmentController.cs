using esbas_internship_backendproject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace esbas_internship_backendproject.Controllers
{
    [ApiController]
    [Route("api")]
    public class User_DepartmentController : Controller
    {
        private readonly EsbasDbContext _context;

        public User_DepartmentController(EsbasDbContext context)
        {
            _context = context;
        }

        [HttpGet("userdepartment")]
        [Produces("application/json")]

        public async Task<ActionResult<IEnumerable<User_Department>>> GetUser_Department()
        {
            var user_department = await _context.User_Department
            .ToListAsync();

            return Ok(user_department);
        }

        [HttpGet("userdepartment/{id}")]
        [Produces("application/json")]

        public async Task<ActionResult<IEnumerable<User_Department>>> GetUser_DepartmentById(int id)
        {
            var user_department = await _context.User_Department.FindAsync(id);

            if(user_department == null) return NotFound();

            return Ok(user_department);
        }

        [HttpPost("userdepartment")]
        [Produces("application/json")]

        public async Task<ActionResult> PostUser_Deparment([FromBody] User_Department user_Department)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT User_Department ON");
                    _context.User_Department.Add(user_Department);
                    await _context.SaveChangesAsync();
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT User_Department OFF");
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            _context.User_Department.Add(user_Department);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser_Department), new { id = user_Department.D_ID }, user_Department);

        }

        [HttpPut("userdeparment/{id}")]
        [Produces("application/json")]

        public async Task<ActionResult> Put_UserDepartment(int id, [FromBody] User_Department user_Department)
        {

            if (id != user_Department.D_ID)
            {
                return BadRequest();
            }

            _context.Entry(user_Department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!User_DepartmentExists(id))
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

        [HttpDelete("userdepartment/{id}")]
        [Produces("application/json")]

        public async Task<ActionResult> SoftDeleteUser_Department(int id)
        {
            var userdepartment = await _context.User_Department.FindAsync(id);

            if (userdepartment == null)
            {
                return NotFound();
            }

            userdepartment.Status = false;
            _context.User_Department.Update(userdepartment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool User_DepartmentExists(int id)
        {
            return _context.User_Department.Any(ud => ud.D_ID == id);
        }
    }
}
