using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SocialInitiatives3.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }


        public DbSet<Initiative> initiatives { get; set; }
        public DbSet<Event> events { get; set; }
        public DbSet<SYOI> ownInitiatives { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<ClubUser> clubUsers { get; set; }
        public DbSet<NGO> nGOs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserVolunteer>()
                .HasKey(t => new {t.userId, t.initiativeId});

            modelBuilder.Entity<UserVolunteer>()
                .HasOne(bc => bc.user)
                .WithMany(b => b.UserVolunteers)
                .HasForeignKey(bc => bc.userId);

            modelBuilder.Entity<UserVolunteer>()
                .HasOne(bc => bc.initiative)
                .WithMany(c => c.UserVolunteers)
                .HasForeignKey(bc => bc.initiativeId);
        }
    }
}