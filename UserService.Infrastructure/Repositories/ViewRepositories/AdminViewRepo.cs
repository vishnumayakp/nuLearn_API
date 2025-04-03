using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.RepoInterfaces.ViewRepo;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Repositories.ViewRepositories
{
    public class AdminViewRepo:IAdminViewRepo
    {
        private readonly AppDbContext _appDbContext;

        public AdminViewRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Admin> GetAdminById(Guid id)
        {
            var admin = await _appDbContext.Admin.FirstOrDefaultAsync(a=>a.Id == id);
            return admin;
        }

        public async Task<bool> AdminExists(Guid adminId)
        {
            return await _appDbContext.Admin
                .AsNoTracking()
                .AnyAsync(a => a.Id == adminId && !a.Is_deleted);
        }

        public async Task<string> GetAdminRole(Guid adminId)
        {
            return await _appDbContext.Admin
                .Where(a => a.Id == adminId)
                .Select(a => a.Role) 
                .FirstOrDefaultAsync();
        }

    }
}
