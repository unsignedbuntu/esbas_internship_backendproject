﻿using esbas_internship_backendproject.DTOs;
using Microsoft.AspNetCore.Mvc;
using esbas_internship_backendproject.Entities;
using AutoMapper;

namespace esbas_internship_backendproject.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class Event_LocationDTOController : ControllerBase
    {

        private readonly EsbasDbContext _context;
        private readonly IMapper _mapper;
        public Event_LocationDTOController(EsbasDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetEventLocations()
        {
            var eventLocations = _context.Event_Location
                .Select(el => _mapper.Map<EventLocationDTO>(el))
                .ToList();

            return Ok(eventLocations);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public IActionResult GetEventLocation(int id)
        {
            var eventLocation = _context.Event_Location
                .Where(el => el.L_ID == id)
                .Select(el => _mapper.Map<EventLocationDTO>(el))
                .FirstOrDefault();

            if (eventLocation == null)
            {
                return NotFound();
            }

            return Ok(eventLocation);
        }

        [HttpPost]
        [Produces("application/json")]
        public IActionResult CreateEventLocation([FromBody] EventLocationDTO eventLocationDTO)
        {
            if (eventLocationDTO == null)
            {
                return BadRequest();
            }

            var eventLocation = new Event_Location
            {
                Name = eventLocationDTO.Name,
            };

            _context.Event_Location.Add(eventLocation);
            _context.SaveChanges();

            return Ok(eventLocation);
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        public IActionResult UpdateEventLocation(int id, [FromBody] EventLocationDTO eventLocationDTO)
        {
            if (eventLocationDTO == null)
            {
                return BadRequest();
            }

            var eventLocation = _context.Event_Location.FirstOrDefault(el => el.L_ID == id);

            if (eventLocation == null)
            {
                return NotFound();
            }

            eventLocation.Name = eventLocationDTO.Name;

            _context.SaveChanges();

            return Ok(eventLocation);
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        public IActionResult SoftDeleteEventLocation(int id)
        {
            var eventLocation = _context.Event_Location.FirstOrDefault(el => el.L_ID == id);

            if (eventLocation == null)
            {
                return NotFound();
            }

            // Kaydın durumunu "deleted" olarak günceller.
            eventLocation.Status = false;


            _context.Event_Location.Update(eventLocation);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
