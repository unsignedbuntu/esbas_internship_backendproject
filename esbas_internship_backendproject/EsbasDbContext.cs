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
        public DbSet<Event_Type> Event_Type { get; set; }
        public DbSet<Event_Location> Event_Location { get; set; }
        public DbSet<User_Department> User_Department {  get; set; }
        public DbSet<User_IsOfficeEmployee> IsOfficeEmployee { get; set; } 
        public DbSet<User_Gender> User_Gender { get; set; }

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

            Modelbuilder.Entity<Event_Type>().HasKey(et  => et.Event_TypeID);
            Modelbuilder.Entity<Event_Type>().ToTable("Event_Type");

            Modelbuilder.Entity<Event_Location>().HasKey(el => el.Event_LocationID);
            Modelbuilder.Entity<Event_Location>().ToTable("Event_Location");

            Modelbuilder.Entity<User_Department>().HasKey(ud => ud.User_DepartmentID);
            Modelbuilder.Entity<User_Department>().ToTable("User_Department");

            Modelbuilder.Entity<User_IsOfficeEmployee>().HasKey(uı => uı.User_IsOfficeEmployeeID);
            Modelbuilder.Entity<User_IsOfficeEmployee>().ToTable("User_IsOfficeEmployee");

            Modelbuilder.Entity<User_Gender>().HasKey(ug => ug.User_GenderID);
            Modelbuilder.Entity<User_Gender>().ToTable("User_Gender");

            Modelbuilder.Entity<Events_Users>()
           .HasKey(eu => new { eu.EventID, eu.UserID });

           /*Modelbuilder.Entity<Events_Users>()
                .HasOne(eu => eu.Event)
                .WithMany(e => e.Users)
                .HasForeignKey(eu => eu.EventID);

            Modelbuilder.Entity<Events_Users>()
                .HasOne(eu => eu.User)
                .WithMany(u => u.Events)
                .HasForeignKey(eu => eu.UserID);

            Modelbuilder.Entity<Events>()
                .HasKey(e => new { e.EventTypeID, e.EventLocationID });

            Modelbuilder.Entity<Events>()
                .HasOne(e => e.)*/
        }
    }
}
