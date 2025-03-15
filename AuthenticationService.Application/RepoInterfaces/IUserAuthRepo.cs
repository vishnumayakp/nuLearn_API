using AuthenticationService.Domain.Entity;
using PlotLink.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.RepoInterfaces
{
    public interface IUserAuthRepo
    {
        Task<User> GetUserByEmail(string email);
        Task<bool> AddUser(User user);
        Task<bool> AddVerifyUser(VerifyUser verifyUser);
        Task<VerifyUser> GetVerifyUserByEmail(string email);
        Task<bool> RemoveVerifyUser(VerifyUser verifyUser);
        Task<bool> SaveChangesAsync();
    }
}
