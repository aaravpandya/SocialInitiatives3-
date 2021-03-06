﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SocialInitiatives3.Models;
using System;

namespace SocialInitiatives3.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20181219083613_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SocialInitiatives3.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("AdmissionNumber");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("House");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Section");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("_class");

                    b.Property<bool>("club_signed_up");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("SocialInitiatives3.Models.ClubUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Class");

                    b.Property<string>("Section");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("clubUsers");
                });

            modelBuilder.Entity("SocialInitiatives3.Models.Event", b =>
                {
                    b.Property<int>("EventID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime?>("End");

                    b.Property<bool>("IsFullDay");

                    b.Property<string>("Organiser");

                    b.Property<string>("OrganiserEmail");

                    b.Property<string>("PhoneNumber");

                    b.Property<DateTime>("Start");

                    b.Property<string>("Subject");

                    b.Property<string>("ThemeColor");

                    b.Property<string>("UserId");

                    b.Property<bool>("Visible");

                    b.HasKey("EventID");

                    b.HasIndex("UserId");

                    b.ToTable("events");
                });

            modelBuilder.Entity("SocialInitiatives3.Models.Initiative", b =>
                {
                    b.Property<int>("InitiativeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.Property<string>("InitiativeAddress")
                        .IsRequired();

                    b.Property<string>("InitiativeName")
                        .IsRequired();

                    b.Property<string>("UserId");

                    b.Property<bool>("Visible");

                    b.Property<int>("categoryId");

                    b.Property<string>("facebookLink");

                    b.Property<string>("filepath")
                        .IsRequired();

                    b.Property<string>("instagramLink");

                    b.Property<string>("phoneNumber");

                    b.Property<string>("team");

                    b.Property<string>("websiteLink");

                    b.Property<string>("work")
                        .IsRequired();

                    b.HasKey("InitiativeId");

                    b.HasIndex("UserId");

                    b.ToTable("initiatives");
                });

            modelBuilder.Entity("SocialInitiatives3.Models.NGO", b =>
                {
                    b.Property<int>("NGOId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.Property<string>("NgoAddress")
                        .IsRequired();

                    b.Property<string>("NgoName")
                        .IsRequired();

                    b.Property<int>("categoryId");

                    b.Property<string>("facebookLink");

                    b.Property<string>("filepath")
                        .IsRequired();

                    b.Property<string>("instagramLink");

                    b.Property<string>("phoneNumber");

                    b.Property<string>("team");

                    b.Property<string>("websiteLink");

                    b.Property<string>("work")
                        .IsRequired();

                    b.HasKey("NGOId");

                    b.ToTable("nGOs");
                });

            modelBuilder.Entity("SocialInitiatives3.Models.SYOI", b =>
                {
                    b.Property<int>("SYOIId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("UserId");

                    b.Property<bool>("Visible");

                    b.Property<string>("cause");

                    b.Property<string>("idea");

                    b.Property<string>("resources");

                    b.Property<string>("targetGroup");

                    b.Property<string>("team");

                    b.HasKey("SYOIId");

                    b.HasIndex("UserId");

                    b.ToTable("ownInitiatives");
                });

            modelBuilder.Entity("SocialInitiatives3.Models.UserVolunteer", b =>
                {
                    b.Property<string>("userId");

                    b.Property<int>("initiativeId");

                    b.HasKey("userId", "initiativeId");

                    b.HasIndex("initiativeId");

                    b.ToTable("UserVolunteer");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SocialInitiatives3.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SocialInitiatives3.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SocialInitiatives3.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SocialInitiatives3.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SocialInitiatives3.Models.Event", b =>
                {
                    b.HasOne("SocialInitiatives3.Models.AppUser", "User")
                        .WithMany("user_events")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SocialInitiatives3.Models.Initiative", b =>
                {
                    b.HasOne("SocialInitiatives3.Models.AppUser", "User")
                        .WithMany("user_initiatives_created")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SocialInitiatives3.Models.SYOI", b =>
                {
                    b.HasOne("SocialInitiatives3.Models.AppUser", "User")
                        .WithMany("sYOIs_user_created")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SocialInitiatives3.Models.UserVolunteer", b =>
                {
                    b.HasOne("SocialInitiatives3.Models.Initiative", "initiative")
                        .WithMany("UserVolunteers")
                        .HasForeignKey("initiativeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SocialInitiatives3.Models.AppUser", "user")
                        .WithMany("UserVolunteers")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
