using AuthenticationService.Application.RepoInterfaces;
using AuthenticationService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PlotLink.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Infrastructure.Repositories
{
    public class UserAuthRepository:IUserAuthRepo
    {
        private readonly AppDbContext _appDbContext;
        public UserAuthRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(e => e.Email == email);
                return user;
            }catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<bool> AddUser(User user)
        {
             await _appDbContext.Users.AddAsync(user);
             return true;
        }

        public async Task<bool> AddVerifyUser(VerifyUser verifyUser)
        {
            await _appDbContext.VerifyUsers.AddAsync(verifyUser);
            return true;
        }

        public async Task<VerifyUser> GetVerifyUserByEmail(string email)
        {
            try
            {
                var verifiedUser = await _appDbContext.VerifyUsers.FirstOrDefaultAsync(v => v.Email == email);
                return verifiedUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<bool> RemoveVerifyUser(VerifyUser verifyUser)
        {
             _appDbContext.VerifyUsers.Remove(verifyUser);
            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
            return true;
        }

    }

}
