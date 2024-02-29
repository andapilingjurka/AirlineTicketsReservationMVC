using AirlineTicketsReservation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AirlineTicketsReservation.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Shteti> Shteti { get; set; }
        public DbSet<Qyteti> Qyteti { get; set; }
        public DbSet<Hoteli> Hoteli { get; set; }
        public DbSet<Kontakti> Kontakti { get; set; }
        public DbSet<Aeroplani> Aeroplanet { get; set; }
        public DbSet<Fluturimi> Fluturimet { get; set; }
        public DbSet<Oferta> Ofertat { get; set; }
        public DbSet<Vetura> Vetura { get; set; }
        public DbSet<Aeroporti> Aeroporti { get; set; }
        public DbSet<RezervimiV> RezervimiV { get; set; }
        public DbSet<Rezervimi> Rezervimet { get; set; }
        public DbSet<RezervimOferta> Rezervimet_me_Oferte { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Seed Roles(User,Admin,SuperAdmin)

            var adminRoleId = "5811453b-6a15-4bd0-82ca-fc98643dfb3b";
            var superAdminRoleId = "8518f0ce-cf65-4517-a2b9-2d8f50f1d5cd";
            var userRoleId = "1a98fd2f-687a-4c4e-93da-b787a8c0ff05";


            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name="Admin",
                    NormalizedName="Admin",
                    Id=adminRoleId,
                    ConcurrencyStamp= adminRoleId
                },
                new IdentityRole
                {
                    Name="SuperAdmin",
                    NormalizedName="SuperAdmin",
                    Id=superAdminRoleId,
                    ConcurrencyStamp=superAdminRoleId
                },
                new IdentityRole
                {
                    Name= "User",
                    NormalizedName = "User",
                    Id=userRoleId,
                    ConcurrencyStamp=userRoleId

                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
            //Seed SuperAdminUser

            var superAdminId = "823ff811-fe7b-4b83-84e2-330ba8905a23";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@gmail.com",
                Email = "superadmin@gmail.com",
                NormalizedEmail = "superadmin@gmail.com".ToUpper(),
                NormalizedUserName = "superadmin@gmail.com".ToUpper(),
                Id = superAdminId
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "Superadmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Add All roles to SuperAdminUser
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId =userRoleId,
                    UserId = superAdminId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);



            ///////////////////////////////////////////////////////////////////////////


            builder.Entity<Qyteti>()
           .HasOne(p => p.Shteti)
           .WithMany()
           .HasForeignKey(p => p.ShtetiId) //Foreign Key
           .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Hoteli>()
           .HasOne(p => p.Qyteti)
           .WithMany()
           .HasForeignKey(p => p.QytetiId) //Foreign Key
           .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Fluturimi>()
           .HasOne(p => p.Aeroplani)
           .WithMany()
           .HasForeignKey(p => p.AeroplaniId) //Foreign Key
           .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Fluturimi>()
            .HasOne(p => p.Qyteti)
            .WithMany()
            .HasForeignKey(p => p.QytetiId) //Foreign Key
            .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Oferta>()
            .HasOne(p => p.Hoteli)
            .WithMany()
            .HasForeignKey(p => p.HoteliId) //Foreign Key
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Oferta>()
            .HasOne(p => p.Fluturimi)
            .WithMany()
            .HasForeignKey(p => p.FluturimiId) //Foreign Key
            .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<RezervimiV>()
         .HasOne(p => p.Aeroporti)
         .WithMany()
         .HasForeignKey(p => p.AeroportiID) //Foreign Key
         .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<RezervimiV>()
         .HasOne(p => p.Vetura)
         .WithMany()
         .HasForeignKey(p => p.VeturaID) //Foreign Key
         .OnDelete(DeleteBehavior.Restrict);

          builder.Entity<Rezervimi>()
        .HasOne(p => p.Fluturimi)
        .WithMany()
        .HasForeignKey(p => p.FluturimiId) //Foreign Key
        .OnDelete(DeleteBehavior.Restrict);

           builder.Entity<RezervimOferta>()
            .HasOne(p => p.Oferta)
            .WithMany()
            .HasForeignKey(p => p.OfertaId) //Foreign Key
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}