using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.RepoInterfaces.ViewRepo;
using UserService.Domain.Entity;

namespace UserService.Infrastructure.Repositories.ViewRepositories
{
    public class UserViewRepo:IUserViewRepo
    {
        private readonly AppDbContext _appDbContext;

        public UserViewRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<User>> GetAllUser()
        {
            try
            {
                var users = await _appDbContext.Users.ToListAsync();
                return users;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUserById(Guid id)
        {
            try
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUserByName(string name)
        {
            try
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Name == name);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> SaveChangeAsyncCustom()
        {
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUserAsyn(User user)
        {
            try
            {
                _appDbContext.Users.Update(user);
                return await _appDbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
