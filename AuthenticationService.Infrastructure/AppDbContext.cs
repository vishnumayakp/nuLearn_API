using AuthenticationService.Domain.Entities;
using AuthenticationService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using PlotLink.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<VerifyUser> VerifyUsers { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}