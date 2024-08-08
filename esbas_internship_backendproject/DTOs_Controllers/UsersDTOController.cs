﻿using esbas_internship_backendproject.DTOs;
using esbas_internship_backendproject.ResponseDTO;
using Microsoft.AspNetCore.Mvc;
using esbas_internship_backendproject.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
                .Include(u => u.User_Gender)
                .Include(u => u.User_Department)
                .Include(u => u.User_IsOfficeEmployee)
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
                .Include(u => u.User_Gender)
                .Include(u => u.User_Department)
                .Include(u => u.User_IsOfficeEmployee)
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
        public IActionResult CreateUsers([FromBody] UserResponseDTO userResponseDTO)
        {
            if(userResponseDTO == null || !ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var usersResponse = _mapper.Map<Users>(userResponseDTO);

            _context.Users.Add(usersResponse);
            _context.SaveChanges();

            return Ok(usersResponse);
        }

        [HttpPut("{cardid}")]
        [Produces("application/json")]
        public IActionResult UpdateUsers(int cardid, [FromBody] UserResponseDTO userResponseDTO)
        {
            if (userResponseDTO == null)
            {
                return BadRequest();
            }

            var usersResponse = _context.Users.FirstOrDefault(u => u.CardID == cardid);

            if (usersResponse == null)
            {
                return NotFound();
            }
            
            usersResponse.CardID = userResponseDTO.CardID;
            usersResponse.FullName = userResponseDTO.FullName;
            usersResponse.Department = userResponseDTO.Department;
            usersResponse.IsOfficeEmployee = userResponseDTO.IsOfficeEmployee;
            usersResponse.Gender = userResponseDTO.Gender;
           
            _context.SaveChanges();

            return Ok(usersResponse);
        }

        [HttpDelete("{cardid}")]
        [Produces("application/json")]
        public IActionResult SoftDeleteUsers(int cardid)
        {
            var users = _context.Users.FirstOrDefault(u => u.CardID == cardid);

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