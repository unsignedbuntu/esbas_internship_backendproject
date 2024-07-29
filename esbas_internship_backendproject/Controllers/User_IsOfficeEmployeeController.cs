using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using esbas_internship_backendproject.Entities;
namespace esbas_internship_backendproject.Controllers
{
    [ApiController]
    [Route("api")]
    public class User_IsOfficeEmployeeController : Controller
    {
        private readonly EsbasDbContext _context;

            public User_IsOfficeEmployeeController(EsbasDbContext context)
            {
            _context = context;
            }

     
                [HttpGet("userisofficeemployee/{id}")]
                [Produces("application/json")]

                public async Task<ActionResult<IEnumerable<User_IsOfficeEmployee>>> GetUser_IsOfficeEmployeeById(int id)
                {
                    var user_isofficeemployee = await _context.User_IsOfficeEmployee.FindAsync(id);

                    if (user_isofficeemployee == null) return NotFound();

                    return Ok(user_isofficeemployee);
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
                            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT User_IsOfficeEmployee ON");
                            _context.User_IsOfficeEmployee.Add(user_IsOfficeEmployee);
                            await _context.SaveChangesAsync();
                            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT User_IsOfficeEmployee OFF");
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
                    return CreatedAtAction(nameof(GetUser_IsOfficeEmployee), new { id = user_IsOfficeEmployee.I_ID }, user_IsOfficeEmployee);

                }

                [HttpPut("userisofficeemployee/{id}")]
                [Produces("application/json")]

                public async Task<ActionResult> Put_UserIsOfficeEmployee(int id, [FromBody] User_IsOfficeEmployee user_IsOfficeEmployee)
                {

                    if (id != user_IsOfficeEmployee.I_ID)
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

                    if (userisofficeemployee == null)
                    {
                        return NotFound();
                    }

                    userisofficeemployee.Status = false;
                    _context.User_IsOfficeEmployee.Update(userisofficeemployee);
                    await _context.SaveChangesAsync();

                    return NoContent();
                }
                private bool User_IsOfficeEmployeeExists(int id)
                {
                    return _context.User_IsOfficeEmployee.Any(uı => uı.I_ID == id);
                }
            }
}

