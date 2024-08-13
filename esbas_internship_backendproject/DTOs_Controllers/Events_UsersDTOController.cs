using esbas_internship_backendproject.DTOs;
using esbas_internship_backendproject.ResponseDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using esbas_internship_backendproject.Entities;
using AutoMapper;


namespace esbas_internship_backendproject.DTOs_Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Events_UsersDTOController : ControllerBase
    {
        private readonly EsbasDbContext _context;
        private readonly IMapper _mapper;
        public Events_UsersDTOController(EsbasDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetEventsUsers()
        {
            var eventsusers = _context.Events_Users
                .Include(eu => eu.Event)
                    .ThenInclude(e => e.Event_Location)
                .Include(eu => eu.Event)
                    .ThenInclude(e => e.Event_Type)
                .Include(eu => eu.User)
                    .ThenInclude(u => u.User_Gender)
                .Include(eu => eu.User)
                    .ThenInclude(u => u.Main_Characteristicts)
                .Include(eu => eu.User)
                    .ThenInclude(u => u.Other_Characteristicts)
                .Include(eu => eu.User)
                  .ThenInclude(u => u.Department)
                        .ThenInclude(d => d.CostCenters)
                .Include(eu => eu.User)
        .           ThenInclude(u => u.Department)
                        .ThenInclude(d => d.Tasks)
                .Select(eu => _mapper.Map<EventsUsersDTO>(eu))
                .ToList();
     

            return Ok(eventsusers);
        }


        [HttpGet("{id}")]
        [Produces("application/json")]
        public IActionResult GetEventsUserByID(int id)
        {
            var eventsUser = _context.Events_Users
                .Include(eu => eu.Event)
                    .ThenInclude(e => e.Event_Location)
                .Include(eu => eu.Event)
                    .ThenInclude(e => e.Event_Type)
                .Include(eu => eu.User)
                    .ThenInclude(u => u.User_Gender)
                .Include(eu => eu.User)
                    .ThenInclude(u => u.Main_Characteristicts)
                .Include(eu => eu.User)
                    .ThenInclude(u => u.Other_Characteristicts)
                .Include(eu => eu.User)
                  .ThenInclude(u => u.Department)
                        .ThenInclude(d => d.CostCenters)
                .Include(eu => eu.User)
        .ThenInclude(u => u.Department)
                        .ThenInclude(d => d.Tasks)
                .Where(eu => eu.ID == id)
                .Select(eu => _mapper.Map<EventsUsersDTO>(eu))
                .FirstOrDefault();

            if (eventsUser == null)
            {
                return NotFound();
            }

            return Ok(eventsUser);
        }


          [HttpPost()]
          [Produces("application/json")]
          public IActionResult CreateEventsUsersMap([FromBody] EventsUsersResponseDTO eventsUsersResponseDTO)
          {
              if (eventsUsersResponseDTO == null || !ModelState.IsValid)
              {
                  return BadRequest(ModelState); 
              }

            var event_UsersResponse = _mapper.Map<Events_Users>(eventsUsersResponseDTO);

              _context.Events_Users.Add(event_UsersResponse);
              _context.SaveChanges();

              return Ok(event_UsersResponse);
          }
        

        [HttpPut("{id}")]
        [Produces("application/json")]
        public IActionResult UpdateEventsUsers(int id, [FromBody] EventsUsersResponseDTO eventsUsersResponseDTO)
        {
            if (eventsUsersResponseDTO == null)
            {
                return BadRequest();
            }

            var eventsUsersResponse = _context.Events_Users.FirstOrDefault(eu => eu.ID == id);

            if (eventsUsersResponse == null)
            {
                return NotFound();
            }

            eventsUsersResponse.EventID = eventsUsersResponseDTO.EventID;
            eventsUsersResponse.CardID = eventsUsersResponseDTO.CardID;
          
       
            _context.SaveChanges();

            return Ok(eventsUsersResponse);
        }

        [HttpDelete("SoftDelete{id}")]
        [Produces("application/json")]
        public IActionResult SoftDeleteEventsUsers(int id)
        {
            var eventsUsers = _context.Events_Users.FirstOrDefault(eu => eu.ID == id);

            if (eventsUsers == null)
            {
                return NotFound();
            }

            eventsUsers.Status = false;


            _context.Events_Users.Update(eventsUsers);
            _context.SaveChanges();

            return NoContent();
        }
    }

  }
