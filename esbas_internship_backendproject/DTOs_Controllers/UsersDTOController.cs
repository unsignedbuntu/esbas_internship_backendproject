using esbas_internship_backendproject.DTOs;
using Microsoft.AspNetCore.Mvc;
using esbas_internship_backendproject.Entities;
using AutoMapper;

namespace esbas_internship_backendproject.DTOs_Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsersDTOController : ControllerBase
    {
        private readonly EsbasDbContext _context;
        private readonly IMapper _mapper;
        public UsersDTOController(EsbasDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetUsers()
        {
            var users = _context.Users
                .Select(u => _mapper.Map<UserDTO>(u))   
                .ToList();

            return Ok(users);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public IActionResult GetUser(int id)
        {
            var users = _context.Users
                .Where(u => u.UserID == id)
                .Select(u => _mapper.Map<UserDTO>(u)) 
                .FirstOrDefault();

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpPost]
        [Produces("application/json")]
        public IActionResult CreateUsers([FromBody] UserDTO userDTO)
        {
            if(userDTO == null || !ModelState.IsValid) 
            {
                return BadRequest();
            }

            var users = new Users
            {
                UserID = userDTO.UserID,
                FullName = userDTO.FullName,
                Department = userDTO.Department,
                IsOfficeEmployee= userDTO.IsOfficeEmployee,
                Gender = userDTO.Gender,
            };

            _context.Users.Add(users);
            _context.SaveChanges();

            return Ok(users);
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        public IActionResult UpdateUsers(int id, [FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest();
            }

            var users = _context.Users.FirstOrDefault(u => u.UserID == id);

            if (users == null)
            {
                return NotFound();
            }

            users.UserID = userDTO.UserID;
            users.FullName = userDTO.FullName;
            users.Department = userDTO.Department;
            users.IsOfficeEmployee = userDTO.IsOfficeEmployee;
            users.Gender = userDTO.Gender;
           
            _context.SaveChanges();

            return Ok(users);
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        public IActionResult SoftDeleteUsers(int id)
        {
            var users = _context.Users.FirstOrDefault(u => u.UserID == id);

            if (users == null)
            {
                return NotFound();
            }

            // Kaydın durumunu "deleted" olarak günceller.
            users.Status = false;


            _context.Users.Update(users);
            _context.SaveChanges();

            return NoContent();
        }

    }
}