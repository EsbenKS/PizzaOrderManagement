using System;
using EventService.Data;
using EventService.Models;
using Microsoft.EntityFrameworkCore;

namespace EventService.Services
{
    public class EventServiceHandler : IEventService
    {
 
        private readonly ApiContext _context;

        public EventServiceHandler(ApiContext context)
        {
            _context = context;
        }
 
        public async void newEventAsync(string eventDescription, int orderId)
        {
            DateTime now = DateTime.Now;

            Event e = new Event();
            e.EventDescription = eventDescription;
            e.OrderId = orderId;
            e.EventDateTime = now;
            _context.Events.Add(e);
            await _context.SaveChangesAsync();
        }

    }
}

