using esbas_internship_backendproject.DTOs;
using Microsoft.AspNetCore.Mvc;
using esbas_internship_backendproject.Entities;
using AutoMapper;

namespace esbas_internship_backendproject.DTOs_Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class User_DepartmentDTOController : ControllerBase
    {
        private readonly EsbasDbContext _context;
        private readonly IMapper _mapper;
        public User_DepartmentDTOController(EsbasDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetUserDepartments()
        {
            var userDepartments = _context.User_Department
                .Select(ud => _mapper.Map<UserDepartmentDTO>(ud))   
                .ToList();

            return Ok(userDepartments);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public IActionResult GetUserDepartmentByID(int id)
        {
            var userDepartment = _context.User_Department
                .Where(ud => ud.D_ID == id)
                .Select(ud => _mapper.Map<UserDepartmentDTO>(ud))
                .FirstOrDefault();

            if (userDepartment == null)
            {
                return NotFound();
            }

            return Ok(userDepartment);
        }

        [HttpPost]
        [Produces("application/json")]
        public IActionResult CreateUserDepartment([FromBody] UserDepartmentDTO user_DepartmentDTO)
        {
            if (user_DepartmentDTO == null)
            {
                return BadRequest();
            }

            var userDepartment = new User_Department
            {
                Name = user_DepartmentDTO.Name
            };

            _context.User_Department.Add(userDepartment);
            _context.SaveChanges();

            return Ok(userDepartment);
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        public IActionResult UpdateUserDepartment(int id, [FromBody] UserDepartmentDTO userDepartmentDTO)
        {
            if (userDepartmentDTO == null)
            {
                return BadRequest();
            }

            var userDepartment = _context.User_Department.FirstOrDefault(ud => ud.D_ID == id);

            if (userDepartment == null)
            {
                return NotFound();
            }

            userDepartment.Name = userDepartmentDTO.Name;

            _context.SaveChanges();

            return Ok(userDepartment);
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        public IActionResult SoftDeleteUserDepartment(int id)
        {
            var userDepartment = _context.User_Department.FirstOrDefault(ud => ud.D_ID == id);

            if (userDepartment == null)
            {
                return NotFound();
            }

            // Kaydın durumunu "deleted" olarak günceller.
            userDepartment.Status = false;


            _context.User_Department.Update(userDepartment);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
