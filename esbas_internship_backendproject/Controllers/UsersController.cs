using esbas_internship_backendproject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
            var users = await _context.Users
            .ToListAsync();

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
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Events ON");
                    _context.Users.Add(users);
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

            _context.Users.Add(users);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsers), new { id = users.UserID }, users);
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

        [HttpDelete("user/{id}")]
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

        [HttpGet("userdepartment")]
        [Produces("application/json")]

        public async Task<ActionResult<IEnumerable<User_Department>>> GetUser_Department()
        {
            var user_department = await _context.User_Department
            .ToListAsync();

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
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Events ON");
                    _context.User_Department.Add(user_Department);
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

            _context.User_Department.Add(user_Department);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser_Department), new { id = user_Department.User_DepartmentID }, user_Department);

        }

        [HttpPut("userdeparment/{id}")]
        [Produces("application/json")]

        public async Task<ActionResult> Put_UserDepartment(int id, [FromBody] User_Department user_Department)
        {

            if (id != user_Department.User_DepartmentID)
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
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Events ON");
                    _context.User_Gender.Add(user_Gender);
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

            _context.User_Gender.Add(user_Gender);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser_Gender), new { id = user_Gender.User_GenderID }, user_Gender);

        }

        [HttpPut("usergender/{id}")]
        [Produces("application/json")]

        public async Task<ActionResult> Put_UserGender(int id, [FromBody] User_Gender user_Gender)
        {

            if (id != user_Gender.User_GenderID)
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

        [HttpGet("userisofficeemployee")]
        [Produces("application/json")]

        public async Task<ActionResult<IEnumerable<User_IsOfficeEmployee>>> GetUser_IsOfficeEmployee()
        {
            var user_isofficeemployee = await _context.User_IsOfficeEmployee
            .ToListAsync();

            return Ok(user_isofficeemployee);
        }

        [HttpPost("userisofficeemployee")]
        [Produces("application/json")]

        public async Task<ActionResult> PostUser_IsOfficeEmployee([FromBody] User_IsOfficeEmployee user_IsOfficeEmployee)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Events ON");
                    _context.User_IsOfficeEmployee.Add(user_IsOfficeEmployee);
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

            _context.User_IsOfficeEmployee.Add(user_IsOfficeEmployee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser_IsOfficeEmployee), new { id = user_IsOfficeEmployee.User_IsOfficeEmployeeID }, user_IsOfficeEmployee);

        }

        [HttpPut("userisofficeemployee/{id}")]
        [Produces("application/json")]

        public async Task<ActionResult> Put_UserIsOfficeEmployee(int id, [FromBody] User_IsOfficeEmployee user_IsOfficeEmployee)
        {

            if (id != user_IsOfficeEmployee.User_IsOfficeEmployeeID)
            {
                return BadRequest();
            }

            _context.Entry(user_IsOfficeEmployee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!User_IsOfficeEmployeeExists(id))
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

        [HttpDelete("userisofficeemployee/{id}")]
        [Produces("application/json")]

        public async Task<ActionResult> SoftDeleteUser_IsOfficeEmployee(int id)
        {
            var userisofficeemployee = await _context.User_IsOfficeEmployee.FindAsync(id);

            if ( userisofficeemployee == null)
            {
                return NotFound();
            }

            userisofficeemployee.Status = false;
            _context.User_IsOfficeEmployee.Update(userisofficeemployee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(u => u.UserID == id);
        }

        private bool User_DepartmentExists(int id)
        {
            return _context.User_Department.Any(ud => ud.User_DepartmentID == id);
        }

        private bool User_IsOfficeEmployeeExists(int id)
        {

            return _context.User_Department.Any( uı => uı.User_DepartmentID == id);
        }

        private bool User_GenderExists(int id)
        {
            return _context.User_Gender.Any( ug => ug.User_GenderID == id);
        }
    }
}
