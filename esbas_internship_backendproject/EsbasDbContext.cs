using esbas_internship_backendproject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection.Emit;

namespace esbas_internship_backendproject
{
    public class EsbasDbContext : DbContext
    {
#nullable disable
        public DbSet<Users> Users { get;  set; }
        public DbSet<Events> Events { get;  set; }
        public DbSet<Events_Users> Events_Users { get; set; }
        public EsbasDbContext(DbContextOptions<EsbasDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder Modelbuilder)
        {
            Modelbuilder.Entity<Users>().HasKey(u => u.UserID);
            Modelbuilder.Entity<Users>().ToTable("Users");

            Modelbuilder.Entity<Events>().HasKey(e => e.EventID);
            Modelbuilder.Entity<Events>().ToTable("Events");

            Modelbuilder.Entity<Events_Users>().HasKey(eu => eu.Events_UserID);
            Modelbuilder.Entity<Events_Users>().ToTable("Events_Users");

            Modelbuilder.Entity<Events_Users>()
           .HasKey(eu => new { eu.EventID, eu.UserID });

           Modelbuilder.Entity<Events_Users>()
                .HasOne(eu => eu.Event)
                .WithMany(e => e.Users)
                .HasForeignKey(eu => eu.EventID);

            Modelbuilder.Entity<Events_Users>()
                .HasOne(eu => eu.User)
                .WithMany(u => u.Events)
                .HasForeignKey(eu => eu.UserID);
        }
    }
}
