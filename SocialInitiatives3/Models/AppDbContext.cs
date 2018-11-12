using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialInitiatives3.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserVolunteer>()
                .HasKey(t => new { t.userId, t.initiativeId });

            modelBuilder.Entity<UserVolunteer>()
                .HasOne(bc => bc.user)
                .WithMany(b => b.UserVolunteers)
                .HasForeignKey(bc => bc.userId);

            modelBuilder.Entity<UserVolunteer>()
                .HasOne(bc => bc.initiative)
                .WithMany(c => c.UserVolunteers)
                .HasForeignKey(bc => bc.initiativeId);
        }


        public DbSet<Initiative> initiatives { get; set; }
        public DbSet<Event> events { get; set; }
        public DbSet<SYOI> ownInitiatives { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<ClubUser> clubUsers { get; set; }
    }
}
