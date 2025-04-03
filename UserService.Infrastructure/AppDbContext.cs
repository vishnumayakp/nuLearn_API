using UserService.Domain.Entities;
using UserService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<VerifyUser> VerifyUsers { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<VerifyInstructor> VerifyInstructors { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Follower> Followers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Follower>()
                .HasOne(f => f.User)
                .WithMany(u => u.Followings)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Follower>()
                .HasOne(i => i.Instructor)
                .WithMany(a => a.Followers)
                .HasForeignKey(f => f.InstructorId);
        }
    }
}