using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using EF02.models;
using Microsoft.EntityFrameworkCore;

namespace EF02
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;Database=EventHubDB;Trusted_Connection=True;");

        }
        #region Dbsets
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<OrganizerProfile> Profiles { get; set; }

        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registrations {  get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Organizer - Profile (Required)
            modelBuilder.Entity<Organizer>()
                .HasOne(o => o.Profile)
                .WithOne(p => p.Organizer)
                .HasForeignKey<OrganizerProfile>(p => p.OrganizerId);

            // Attendee - Badge (Optional)
            modelBuilder.Entity<Attendee>()
                .HasOne(a => a.Badge)
                .WithOne(b => b.Attendee)
                .HasForeignKey<Badge>(b => b.AttendeeId);

            //  One-to-Many Organizer-Events
            modelBuilder.Entity<Organizer>()
                .HasMany(o => o.Events)
                .WithOne(e => e.Organizer)
                .HasForeignKey(e => e.OrganizerId);

            // Self Relation Event-Sessions
            modelBuilder.Entity<Event>()
                .HasOne(e => e.ParentEvent)
                .WithMany(e => e.Sessions)
                .HasForeignKey(e => e.ParentEventId);

            //  Many-to-Many via Registration
            modelBuilder.Entity<Registration>()
                .HasKey(r => new { r.AttendeeId, r.EventId });

            modelBuilder.Entity<Registration>()
            .HasOne(r => r.Attendee)
            .WithMany(a => a.Registrations)
            .HasForeignKey(r => r.AttendeeId);

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Event)
                .WithMany(e => e.Registrations)
                .HasForeignKey(r => r.EventId);
        }
    }
}
