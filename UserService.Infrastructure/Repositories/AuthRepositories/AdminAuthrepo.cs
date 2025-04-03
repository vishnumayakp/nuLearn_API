using UserService.Application.RepoInterfaces;
using UserService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.RepoInterfaces.AuthRepo;

namespace UserService.Infrastructure.Repositories
{
    public class AdminAuthrepo:IAdminAuthRpo
    {
        private readonly AppDbContext _dbContext;

        public AdminAuthrepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Admin> GetAdminorByEmail(string email)
        {
            try
            {
                var admin = await _dbContext.Admin.FirstOrDefaultAsync(e=>e.Email==email);
                return admin;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching instructor: {ex.Message}");
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
