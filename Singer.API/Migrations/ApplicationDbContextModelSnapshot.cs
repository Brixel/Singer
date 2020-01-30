﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Singer.Data;
using Singer.Models;

namespace Singer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Singer.Models.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AllowedAgeGroups");

                    b.Property<decimal>("Cost");

                    b.Property<DateTime>("DayCareAfterEndDateTime");

                    b.Property<DateTime>("DayCareBeforeStartDateTime");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndRegistrationDateTime");

                    b.Property<DateTime>("FinalCancellationDateTime");

                    b.Property<bool>("HasDayCareAfter");

                    b.Property<bool>("HasDayCareBefore");

                    b.Property<bool>("IsArchived");

                    b.Property<Guid>("LocationId");

                    b.Property<int>("MaxRegistrants");

                    b.Property<bool>("RegistrationOnDailyBasis");

                    b.Property<DateTime>("StartRegistrationDateTime");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Singer.Models.EventLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Name");

                    b.Property<string>("PostalCode");

                    b.HasKey("Id");

                    b.ToTable("EventLocations");
                });

            modelBuilder.Entity("Singer.Models.EventRegistrationLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDateTimeUTC");

                    b.Property<bool>("EmailSent");

                    b.Property<int>("EventRegistrationChanges");

                    b.Property<Guid>("EventRegistrationId");

                    b.Property<Guid>("ExecutedByUserId");

                    b.HasKey("Id");

                    b.HasIndex("EventRegistrationId");

                    b.HasIndex("ExecutedByUserId");

                    b.ToTable("EventRegistrationLogs");

                    b.HasDiscriminator<int>("EventRegistrationChanges");
                });

            modelBuilder.Entity("Singer.Models.EventSlot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDateTime");

                    b.Property<Guid>("EventId");

                    b.Property<DateTime>("StartDateTime");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("EventSlots");
                });

            modelBuilder.Entity("Singer.Models.Registration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CareUserId");

                    b.Property<Guid?>("DaycareLocationId");

                    b.Property<DateTime>("EndDateTime");

                    b.Property<int>("EventRegistrationType");

                    b.Property<Guid?>("EventSlotId");

                    b.Property<DateTime>("StartDateTime");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("DaycareLocationId");

                    b.HasIndex("EventSlotId");

                    b.HasIndex("CareUserId", "EventSlotId")
                        .IsUnique()
                        .HasFilter("[EventSlotId] IS NOT NULL");

                    b.ToTable("EventRegistrations");
                });

            modelBuilder.Entity("Singer.Models.Users.AdminUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AdminUsers");
                });

            modelBuilder.Entity("Singer.Models.Users.CareUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AgeGroup");

                    b.Property<DateTime>("BirthDay");

                    b.Property<string>("CaseNumber");

                    b.Property<bool>("HasTrajectory");

                    b.Property<bool>("IsExtern");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CareUsers");
                });

            modelBuilder.Entity("Singer.Models.Users.LegalGuardianCareUser", b =>
                {
                    b.Property<Guid>("CareUserId");

                    b.Property<Guid>("LegalGuardianId");

                    b.HasKey("CareUserId", "LegalGuardianId");

                    b.HasIndex("LegalGuardianId");

                    b.ToTable("LegalGuardianCareUsers");
                });

            modelBuilder.Entity("Singer.Models.Users.LegalGuardianUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<bool>("IsArchived");

                    b.Property<string>("PostalCode");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("LegalGuardianUsers");
                });

            modelBuilder.Entity("Singer.Models.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Singer.Models.EventRegistrationLocationChange", b =>
                {
                    b.HasBaseType("Singer.Models.EventRegistrationLog");

                    b.Property<Guid>("NewLocationId");

                    b.Property<Guid>("PreviousLocationId");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Singer.Models.EventRegistrationStatusChange", b =>
                {
                    b.HasBaseType("Singer.Models.EventRegistrationLog");

                    b.Property<int>("NewStatus");

                    b.Property<int>("PreviousStatus");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Singer.Models.Users.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Singer.Models.Users.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Singer.Models.Users.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Singer.Models.Users.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Singer.Models.Event", b =>
                {
                    b.HasOne("Singer.Models.EventLocation", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Singer.Models.EventRegistrationLog", b =>
                {
                    b.HasOne("Singer.Models.Registration", "EventRegistration")
                        .WithMany()
                        .HasForeignKey("EventRegistrationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Singer.Models.Users.User", "ExecutedByUser")
                        .WithMany()
                        .HasForeignKey("ExecutedByUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Singer.Models.EventSlot", b =>
                {
                    b.HasOne("Singer.Models.Event", "Event")
                        .WithMany("EventSlots")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Singer.Models.Registration", b =>
                {
                    b.HasOne("Singer.Models.Users.CareUser", "CareUser")
                        .WithMany("EventRegistrations")
                        .HasForeignKey("CareUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Singer.Models.EventLocation", "DaycareLocation")
                        .WithMany()
                        .HasForeignKey("DaycareLocationId");

                    b.HasOne("Singer.Models.EventSlot", "EventSlot")
                        .WithMany("Registrations")
                        .HasForeignKey("EventSlotId");
                });

            modelBuilder.Entity("Singer.Models.Users.AdminUser", b =>
                {
                    b.HasOne("Singer.Models.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Singer.Models.Users.CareUser", b =>
                {
                    b.HasOne("Singer.Models.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Singer.Models.Users.LegalGuardianCareUser", b =>
                {
                    b.HasOne("Singer.Models.Users.CareUser", "CareUser")
                        .WithMany("LegalGuardianCareUsers")
                        .HasForeignKey("CareUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Singer.Models.Users.LegalGuardianUser", "LegalGuardian")
                        .WithMany("LegalGuardianCareUsers")
                        .HasForeignKey("LegalGuardianId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Singer.Models.Users.LegalGuardianUser", b =>
                {
                    b.HasOne("Singer.Models.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
