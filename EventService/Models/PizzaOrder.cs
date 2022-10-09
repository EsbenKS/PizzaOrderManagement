using System;
namespace EventService.Models
{
    public class PizzaOrder
    {
        public int Id { get; set; }

        public int TableId { get; set; }

        public int? PizzaId { get; set; }

        public string? SpecialRequests { get; set; }

    }

}
