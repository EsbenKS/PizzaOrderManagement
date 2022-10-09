namespace EventService.Models
{

    public class Event
    {
        public int Id { get; set; }

        public string? EventDescription { get; set; }

        public DateTime? EventDateTime { get; set; }

        public int OrderId { get; set; }
    }

}
