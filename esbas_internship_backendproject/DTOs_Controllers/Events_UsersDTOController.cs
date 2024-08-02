using esbas_internship_backendproject.DTOs;
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
                .Include(eu => eu.User)
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
                .Include(eu => eu.User)
                .Where(eu => eu.ID == id)
                .Select(eu => _mapper.Map<EventsUsersDTO>(eu))
                .FirstOrDefault();

            if (eventsUser == null)
            {
                return NotFound();
            }

            return Ok(eventsUser);
        }


            [HttpPost]
            [Produces("application/json")]
            public IActionResult CreateEventsUsers([FromBody] EventsUsersDTO eventsUsersDTO)
            {
                if (eventsUsersDTO == null || !ModelState.IsValid)
                {
                return BadRequest(ModelState); // Hataları döndür  
                }

           
                var event_Users = _mapper.Map<Events_Users>(eventsUsersDTO);

                _context.Events_Users.Add(event_Users);
                _context.SaveChanges();

                return Ok(_mapper.Map<EventsUsersDTO>(event_Users));
        }

        [HttpPost("CreateWithMapper")]
        [Produces("application/json")]
        public IActionResult CreateEventsUsersMap([FromBody] EventsUsersDTO eventsUsersDTO)
        {
            if (eventsUsersDTO == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var event_Users = _mapper.Map<Events_Users>(eventsUsersDTO);

            _context.Events_Users.Add(event_Users);
            _context.SaveChanges();

            return Ok(_mapper.Map<EventsUsersDTO>(event_Users));
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        public IActionResult UpdateEventsUsers(int id, [FromBody] EventsUsersDTO events_UsersDTO)
        {
            if (events_UsersDTO == null)
            {
                return BadRequest();
            }

            var eventsUsers = _context.Events_Users.FirstOrDefault(eu => eu.ID == id);

            if (eventsUsers == null)
            {
                return NotFound();
            }

            eventsUsers.ID = events_UsersDTO.ID;
            eventsUsers.EventID = events_UsersDTO.EventID;
            eventsUsers.UserID = events_UsersDTO.UserID;
          
            _context.SaveChanges();

            return Ok(eventsUsers);
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

        [HttpDelete("ForceDelete{eventId}/{userId}")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteEvents_Users(int eventId, int userId)
        {
            var eventUser = await _context.Events_Users
                .FirstOrDefaultAsync(eu => eu.EventID == eventId && eu.UserID == userId);

            if (eventUser == null)
            {
                return NotFound();
            }

            _context.Events_Users.Remove(eventUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }

  }
