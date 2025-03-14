using AuthenticationService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Domain.Interface
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmail(string email);
        Task AddUser(User user);
        Task UpdateUser(User user);
    }
}
