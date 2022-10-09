using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventService.Data;
using EventService.Models;
using EventService.Services;

namespace EventService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewEventController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IEventService _eventService;


        public NewEventController(ApiContext context, IEventService eventService)
        {
            _context = context;
            _eventService = eventService;
        }

        /// <summary>
        /// Henter events
        /// </summary>
        /// <param name="startIndex">Index på det første event der skal hentes</param>
        /// <param name="antal">Antallet af events der maksimalt skal hentes (der kan være færre)</param>
        /// <returns></returns>
        ///

        //
        [HttpGet("{startIndex}/{NumberOfEvents}")]
        public async Task<ActionResult<IEnumerable<Event>>> ListEvents(int startIndex, int NumberOfEvents)
        {

            var events = await _context.Events
                .Where(e => e.Id >= startIndex).Take(NumberOfEvents).ToListAsync();

            if (events == null)
            {
                return NotFound();
            }

            return events;

        }

        // GET: api/NewEvent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
          if (_context.Events == null)
          {
              return NotFound();
          }
            return await _context.Events.ToListAsync();
        }

        // GET: api/NewEvent/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
          if (_context.Events == null)
          {
              return NotFound();
          }
            var @event = await _context.Events.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }


        // POST: api/NewEvent
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("RaiseEvent")]
        public async Task<ActionResult<Event>> RaiseEvent(Event @event)
        {
          if (_context.Events == null)
          {
              return Problem("Entity set 'ApiContext.Events'  is null.");
          }
            _eventService.newEventAsync(@event.EventDescription, @event.OrderId);
            return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
        }

        // DELETE: api/NewEvent/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            if (_context.Events == null)
            {
                return NotFound();
            }
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(int id)
        {
            return (_context.Events?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
