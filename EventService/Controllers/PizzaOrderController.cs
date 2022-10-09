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
    public class PizzaOrderController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IEventService _eventService;

        public PizzaOrderController(ApiContext context, IEventService eventService)
        {
            _context = context;
            _eventService = eventService;
        }

        // GET: api/PizzaOrder
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaOrder>>> GetPizzaOrders()
        {
          if (_context.PizzaOrders == null)
          {
              return NotFound();
          }
            return await _context.PizzaOrders.ToListAsync();
        }

        // GET: api/PizzaOrder/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PizzaOrder>> GetPizzaOrder(int id)
        {
          if (_context.PizzaOrders == null)
          {
              return NotFound();
          }
            var pizzaOrder = await _context.PizzaOrders.FindAsync(id);
            _eventService.newEventAsync("Order Reviewed", pizzaOrder.Id);

            if (pizzaOrder == null)
            {
                return NotFound();
            }

            return pizzaOrder;
        }


        // POST: api/PizzaOrder
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PizzaOrder>> PostPizzaOrder(PizzaOrder pizzaOrder)
        {
          if (_context.PizzaOrders == null)
          {
              return Problem("Entity set 'ApiContext.PizzaOrders'  is null.");
          }

            _context.PizzaOrders.Add(pizzaOrder);
            await _context.SaveChangesAsync();

            _eventService.newEventAsync("Order Created", pizzaOrder.Id);

            return CreatedAtAction("GetPizzaOrder", new { id = pizzaOrder.Id }, pizzaOrder);
        }

        // DELETE: api/PizzaOrder/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizzaOrder(int id)
        {
            if (_context.PizzaOrders == null)
            {
                return NotFound();
            }
            var pizzaOrder = await _context.PizzaOrders.FindAsync(id);
            if (pizzaOrder == null)
            {
                return NotFound();
            }
   
            _context.PizzaOrders.Remove(pizzaOrder);
            await _context.SaveChangesAsync();

            _eventService.newEventAsync("Order Deleted", pizzaOrder.Id);

            return NoContent();
        }

        private bool PizzaOrderExists(int id)
        {
            return (_context.PizzaOrders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
