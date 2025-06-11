using Project.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Project.Data
{
    public class GolestanDbContext:DbContext
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
    }
}
