using Events.DataAccess.Configurations;
using Events.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.DataAccess
{
    public class EventsDbContext : DbContext
    {
        public EventsDbContext(DbContextOptions<EventsDbContext> options)
            : base (options) { }

        public DbSet<EventEntity> Events { get; set; }

        public DbSet<ParticipantEntity> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new ParticipantConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
