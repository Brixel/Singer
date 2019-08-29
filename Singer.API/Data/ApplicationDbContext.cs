using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Singer.Models;
using Singer.Models.Users;

namespace Singer.Data
{
   public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
   {
      public DbSet<CareUser> CareUsers { get; set; }
      public DbSet<LegalGuardianUser> LegalGuardianUsers { get; set; }
      public DbSet<LegalGuardianCareUser> LegalGuardianCareUsers { get; set; }
      public DbSet<EventLocation> EventLocations { get; set; }
      public DbSet<Event> Events { get; set; }

      public DbSet<AdminUser> AdminUsers { get; set; }
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
      {
      }

      protected override void OnModelCreating(ModelBuilder builder)
      {
         base.OnModelCreating(builder);
         builder.Entity<LegalGuardianCareUser>()
            .HasKey(x => new { x.CareUserId, x.LegalGuardianId });
         builder.Entity<LegalGuardianCareUser>()
            .HasOne(x => x.LegalGuardian)
            .WithMany(x => x.LegalGuardianCareUsers)
            .OnDelete(DeleteBehavior.Restrict);
         builder.Entity<LegalGuardianCareUser>()
            .HasOne(x => x.CareUser)
            .WithMany(x => x.LegalGuardianCareUsers)
            .OnDelete(DeleteBehavior.Restrict);

      }
   }
}
