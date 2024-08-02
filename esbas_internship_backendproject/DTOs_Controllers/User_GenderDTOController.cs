using esbas_internship_backendproject.DTOs;
using Microsoft.AspNetCore.Mvc;
using esbas_internship_backendproject.Entities;
using AutoMapper;

namespace esbas_internship_backendproject.DTOs_Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class User_GenderDTOController : ControllerBase
    {
        private readonly EsbasDbContext _context;
        private readonly IMapper _mapper;
        public User_GenderDTOController(EsbasDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetUserGenders()
        {
            var userGenders = _context.User_Gender
                .Select(ug => _mapper.Map<UserGenderDTO>(ug))
                .ToList();

            return Ok(userGenders);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public IActionResult GetUserGender(int id)
        {
            var userGender = _context.User_Gender
                .Where(ug => ug.G_ID == id)
                .Select(ug => _mapper.Map<UserGenderDTO>(ug))
               .FirstOrDefault();

            if (userGender == null)
            {
                return NotFound();
            }

            return Ok(userGender);
        }

        [HttpPost]
        [Produces("application/json")]
        public IActionResult CreateUserGender([FromBody] UserGenderDTO userGenderDTO)
        {
            if (userGenderDTO == null)
            {
                return BadRequest();
            }

            var user_Gender = new User_Gender
            {
                Name = userGenderDTO.Name,
            };

            _context.User_Gender.Add(user_Gender);
            _context.SaveChanges();

            return Ok(user_Gender);
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        public IActionResult UpdateUserGender(int id, [FromBody] UserGenderDTO userGenderDTO)
        {
            if (userGenderDTO == null)
            {
                return BadRequest();
            }

            var userGender = _context.User_Gender.FirstOrDefault(ug => ug.G_ID == id);

            if (userGender == null)
            {
                return NotFound();
            }

            userGender.Name = userGenderDTO.Name;

            _context.SaveChanges();

            return Ok(userGender);
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        public IActionResult SoftDeleteUserGender(int id)
        {
            var userGender = _context.User_Gender.FirstOrDefault(ug => ug.G_ID == id);

            if (userGender == null)
            {
                return NotFound();
            }

            // Kaydın durumunu "deleted" olarak günceller.
            userGender.Status = false;


            _context.User_Gender.Update(userGender);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
