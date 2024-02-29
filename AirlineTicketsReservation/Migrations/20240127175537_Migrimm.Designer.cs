﻿// <auto-generated />
using System;
using AirlineTicketsReservation.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AirlineTicketsReservation.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240127175537_Migrimm")]
    partial class Migrimm
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AirlineTicketsReservation.Models.Aeroplani", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Kompania")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Nr_Uleseve_Biznes")
                        .HasColumnType("int");

                    b.Property<int>("Nr_Uleseve_Ekonomike")
                        .HasColumnType("int");

                    b.Property<int>("Nr_Uleseve_VIP")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Aeroplanet");
                });

            modelBuilder.Entity("AirlineTicketsReservation.Models.Aeroporti", b =>
                {
                    b.Property<int>("AeroportiID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AeroportiID"), 1L, 1);

                    b.Property<string>("Emri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QytetiID")
                        .HasColumnType("int");

                    b.HasKey("AeroportiID");

                    b.HasIndex("QytetiID")
                        .IsUnique();

                    b.ToTable("Aeroporti");
                });

            modelBuilder.Entity("AirlineTicketsReservation.Models.Fluturimi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AeroplaniId")
                        .HasColumnType("int");

                    b.Property<string>("ArrivalAirport")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Cmimi")
                        .HasColumnType("int");

                    b.Property<string>("DeparuteAirport")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("KohaEArritjes")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("KohaENisjes")
                        .HasColumnType("datetime2");

                    b.Property<string>("NrFluturimit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QytetiId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AeroplaniId");

                    b.HasIndex("QytetiId");

                    b.ToTable("Fluturimet");
                });

            modelBuilder.Entity("AirlineTicketsReservation.Models.Hoteli", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Adresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Emri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pershkrimi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QytetiId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QytetiId");

                    b.ToTable("Hoteli");
                });

            modelBuilder.Entity("AirlineTicketsReservation.Models.Kontakti", b =>
                {
                    b.Property<int>("KontaktiID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KontaktiID"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Emri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mbiemri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mesazhi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefoni")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KontaktiID");

                    b.ToTable("Kontakti");
                });

            modelBuilder.Entity("AirlineTicketsReservation.Models.Oferta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Cmimi")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FluturimiId")
                        .HasColumnType("int");

                    b.Property<int>("HoteliId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FluturimiId");

                    b.HasIndex("HoteliId");

                    b.ToTable("Ofertat");
                });

            modelBuilder.Entity("AirlineTicketsReservation.Models.Qyteti", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Emri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShtetiId")
                        .HasColumnType("int");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ShtetiId");

                    b.ToTable("Qyteti");
                });

            modelBuilder.Entity("AirlineTicketsReservation.Models.Rezervimi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<long>("Cmimi")
                        .HasColumnType("bigint");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Data_e_Kthimit")
                        .HasColumnType("datetime2");

                    b.Property<DateTimeOffset>("Data_e_Rezervimit")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmriPasagjerit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FluturimiId")
                        .HasColumnType("int");

                    b.Property<string>("Klasi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Kthyese")
                        .HasColumnType("bit");

                    b.Property<string>("MbiemriPasagjerit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("FluturimiId");

                    b.HasIndex("UserId");

                    b.ToTable("Rezervimet");
                });

            modelBuilder.Entity("AirlineTicketsReservation.Models.RezervimiV", b =>
                {
                    b.Property<int>("RezervimiID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RezervimiID"), 1L, 1);

                    b.Property<int>("AeroportiID")
                        .HasColumnType("int");

                    b.Property<decimal>("Cmimi")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DataFillimit")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataKthimit")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("VeturaID")
                        .HasColumnType("int");

                    b.HasKey("RezervimiID");

                    b.HasIndex("AeroportiID");

                    b.HasIndex("UserId");

                    b.HasIndex("VeturaID");

                    b.ToTable("RezervimiV");
                });

            modelBuilder.Entity("AirlineTicketsReservation.Models.RezervimOferta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Cmimi")
                        .HasColumnType("int");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Data_E_Kthimit")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Data_E_Rezervimit")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Emri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mbiemri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OfertaId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("OfertaId");

                    b.HasIndex("UserId");

                    b.ToTable("Rezervimet_me_Oferte");
                });

            modelBuilder.Entity("AirlineTicketsReservation.Models.Shteti", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Emri")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Shteti");
                });

            modelBuilder.Entity("AirlineTicketsReservation.Models.Vetura", b =>
                {
                    b.Property<int>("VeturaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VeturaID"), 1L, 1);

                    b.Property<string>("Cmimi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Karburanti")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modeli")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PershkrimiModelit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VitiProdhimit")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VeturaID");

                    b.ToTable("Vetura");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "5811453b-6a15-4bd0-82ca-fc98643dfb3b",
                            ConcurrencyStamp = "5811453b-6a15-4bd0-82ca-fc98643dfb3b",
                            Name = "Admin",
                            NormalizedName = "Admin"
                        },
                        new
                        {
                            Id = "8518f0ce-cf65-4517-a2b9-2d8f50f1d5cd",
                            ConcurrencyStamp = "8518f0ce-cf65-4517-a2b9-2d8f50f1d5cd",
                            Name = "SuperAdmin",
                            NormalizedName = "SuperAdmin"
                        },
                        new
                        {
                            Id = "1a98fd2f-687a-4c4e-93da-b787a8c0ff05",
                            ConcurrencyStamp = "1a98fd2f-687a-4c4e-93da-b787a8c0ff05",
                            Name = "User",
                            NormalizedName = "User"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "823ff811-fe7b-4b83-84e2-330ba8905a23",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "14fcad71-4835-49b6-95f1-e78a9447635c",
                            Email = "superadmin@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "SUPERADMIN@GMAIL.COM",
                            NormalizedUserName = "SUPERADMIN@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEA/UiglwiY8qq5PWpJ7mHZmJoSMxiRjV6WRSfCVbW3v52VLvXdm7l90DjsiFeoKDMA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "7f874e04-09a2-4de0-b068-e1b23283a51c",
                            TwoFactorEnabled = false,
                            UserName = "superadmin@gmail.com"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "823ff811-fe7b-4b83-84e2-330ba8905a23",
                            RoleId = "5811453b-6a15-4bd0-82ca-fc98643dfb3b"
                        },
                        new
                        {
                            UserId = "823ff811-fe7b-4b83-84e2-330ba8905a23",
                            RoleId = "8518f0ce-cf65-4517-a2b9-2d8f50f1d5cd"
                        },
                        new
                        {
                            UserId = "823ff811-fe7b-4b83-84e2-330ba8905a23",
                            RoleId = "1a98fd2f-687a-4c4e-93da-b787a8c0ff05"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("AirlineTicketsReservation.Models.Aeroporti", b =>
                {
                    b.HasOne("AirlineTicketsReservation.Models.Qyteti", "Qyteti")
                        .WithOne("Aeroporti")
                        .HasForeignKey("AirlineTicketsReservation.Models.Aeroporti", "QytetiID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Qyteti");
                });

            modelBuilder.Entity("AirlineTicketsReservation.Models.Fluturimi", b =>
                {
                    b.HasOne("AirlineTicketsReservation.Models.Aeroplani", "Aeroplani")
                        .WithMany()
                        .HasForeignKey("AeroplaniId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AirlineTicketsReservation.Models.Qyteti", "Qyteti")
                        .WithMany()
                        .HasForeignKey("QytetiId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Aeroplani");

                    b.Navigation("Qyteti");
                });

            modelBuilder.Entity("AirlineTicketsReservation.Models.Hoteli", b =>
                {
                    b.HasOne("AirlineTicketsReservation.Models.Qyteti", "Qyteti")
                        .WithMany()
                        .HasForeignKey("QytetiId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Qyteti");
                });

            modelBuilder.Entity("AirlineTicketsReservation.Models.Oferta", b =>
                {
                    b.HasOne("AirlineTicketsReservation.Models.Fluturimi", "Fluturimi")
                        .WithMany()
                        .HasForeignKey("FluturimiId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AirlineTicketsReservation.Models.Hoteli", "Hoteli")
                        .WithMany()
                        .HasForeignKey("HoteliId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Fluturimi");

                    b.Navigation("Hoteli");
                });

            modelBuilder.Entity("AirlineTicketsReservation.Models.Qyteti", b =>
                {
                    b.HasOne("AirlineTicketsReservation.Models.Shteti", "Shteti")
                        .WithMany()
                        .HasForeignKey("ShtetiId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Shteti");
                });

            modelBuilder.Entity("AirlineTicketsReservation.Models.Rezervimi", b =>
                {
                    b.HasOne("AirlineTicketsReservation.Models.Fluturimi", "Fluturimi")
                        .WithMany()
                        .HasForeignKey("FluturimiId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Fluturimi");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AirlineTicketsReservation.Models.RezervimiV", b =>
                {
                    b.HasOne("AirlineTicketsReservation.Models.Aeroporti", "Aeroporti")
                        .WithMany()
                        .HasForeignKey("AeroportiID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("AirlineTicketsReservation.Models.Vetura", "Vetura")
                        .WithMany()
                        .HasForeignKey("VeturaID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Aeroporti");

                    b.Navigation("User");

                    b.Navigation("Vetura");
                });

            modelBuilder.Entity("AirlineTicketsReservation.Models.RezervimOferta", b =>
                {
                    b.HasOne("AirlineTicketsReservation.Models.Oferta", "Oferta")
                        .WithMany()
                        .HasForeignKey("OfertaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Oferta");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AirlineTicketsReservation.Models.Qyteti", b =>
                {
                    b.Navigation("Aeroporti");
                });
#pragma warning restore 612, 618
        }
    }
}
