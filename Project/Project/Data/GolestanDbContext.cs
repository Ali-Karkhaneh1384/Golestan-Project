using Project.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Golestan_Project.Data
{
    public class GolestanDbContext : DbContext
    {
        public GolestanDbContext(DbContextOptions<GolestanDbContext> option) : base(option) { }
        public DbSet<roles> roles { get; set; }
        public DbSet<user_roles> user_roles { get; set; }
        public DbSet<users> users { get; set; }
        public DbSet<instructors> instructors { get; set; }
        public DbSet<teach> teaches { get; set; }
        public DbSet<sections> sections { get; set; }
        public DbSet<courses> courses { get; set; }
        public DbSet<classrooms> classrooms { get; set; }
        public DbSet<time_slots> timeslots { get; set; }
        public DbSet<students> students { get; set; }
        public DbSet<takes> takes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<user_roles>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<teach>()
                .HasKey(t => new { t.instructor_id, t.section_id });

            modelBuilder.Entity<takes>()
                .HasKey(t => new { t.student_id, t.section_id });

            modelBuilder.Entity<sections>()
                .HasIndex(s => s.course_id)
                .IsUnique(false);

            
            modelBuilder.Entity<roles>().HasData(
                new roles { Id = 1, name = Roles.Admin },
                new roles { Id = 2, name = Roles.Instructor },
                new roles { Id = 3, name = Roles.Student }
            );


            modelBuilder.Entity<users>().HasData(
            new users
            {
                Id = 1,
                Email = "admin@example.com",
                First_Name = "Admin",
                Last_Name = "User",
                Created_at = new DateTime(2024, 01, 01), 
                Hashed_Password = "Admin@1234" 
            }
            );


            modelBuilder.Entity<user_roles>().HasData(
                new user_roles { UserId = 1, RoleId = 1 } 
            );
        }
    }
}
