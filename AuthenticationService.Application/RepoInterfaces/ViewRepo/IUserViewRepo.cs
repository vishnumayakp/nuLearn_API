using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entity;

namespace UserService.Application.RepoInterfaces.ViewRepo
{
    public interface IUserViewRepo
    {
        Task<List<User>> GetAllUser();
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByName(string name);
        Task<bool> SaveChangeAsyncCustom();
        Task<bool> UpdateUserAsyn(User user);
    }
}
