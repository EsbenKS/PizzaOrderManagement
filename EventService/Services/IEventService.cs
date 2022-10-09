using EventService.Models;

namespace EventService.Services
{
    public interface IEventService
    {
        void newEventAsync(String eventDescription, int orderId);
    }
}

