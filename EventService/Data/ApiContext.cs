using System;
using Microsoft.EntityFrameworkCore;
using EventService.Models;

namespace EventService.Data
{
    public class ApiContext : DbContext
    { 
        public DbSet<Event> Events { get; set; }
        public DbSet<PizzaOrder> PizzaOrders { get; set; }


        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new DbInitializer(modelBuilder).Seed();
        }


    }
}

