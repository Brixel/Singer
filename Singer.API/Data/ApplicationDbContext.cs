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
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
      {
      }

      public DbSet<CareUser> CareUsers { get; set; }
      public DbSet<LegalGuardianUser> LegalGuardianUsers { get; set; }
      public DbSet<LegalGuardianCareUser> LegalGuardianCareUsers { get; set; }
      public DbSet<EventLocation> EventLocations { get; set; }
      public DbSet<Event> Events { get; set; }
      public DbSet<EventSlot> EventSlots { get; set; }
      public DbSet<Registration> Registrations { get; set; }
      public DbSet<AdminUser> AdminUsers { get; set; }

      public DbSet<EventRegistrationLog> EventRegistrationLogs { get; set; }

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

         builder.Entity<Event>()
            .HasMany(x => x.EventSlots)
            .WithOne(x => x.Event)
            .OnDelete(DeleteBehavior.Restrict);

         builder.Entity<Registration>()
            .HasOne(x => x.EventSlot)
            .WithMany(x => x.Registrations);

         builder.Entity<Registration>()
            .HasIndex(x => new { x.CareUserId, x.EventSlotId }).IsUnique();

         builder.Entity<CareUser>()
            .HasMany(x => x.EventRegistrations)
            .WithOne(x => x.CareUser)
            .OnDelete(DeleteBehavior.Restrict);

         builder.Entity<EventRegistrationLog>()
            .ToTable("EventRegistrationLogs")
            .HasDiscriminator(x => x.EventRegistrationChanges)
            .HasValue<EventRegistrationLocationChange>(EventRegistrationChanges.LocationChange)
            .HasValue<EventRegistrationStatusChange>(EventRegistrationChanges.RegistrationStatusChange);

         builder.Entity<EventRegistrationLog>()
            .HasOne(x => x.EventRegistration);

         builder.Entity<EventRegistrationStatusChange>()
            .HasBaseType<EventRegistrationLog>();
         builder.Entity<EventRegistrationLocationChange>()
            .HasBaseType<EventRegistrationLog>();
      }
   }
}
