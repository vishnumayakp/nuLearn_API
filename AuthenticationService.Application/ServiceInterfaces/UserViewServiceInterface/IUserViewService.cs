using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.ServiceInterfaces.UserViewServiceInterface
{
    public interface IUserViewService
    {
        Task<bool> UpdateUserProfile(Guid userId, string name, IFormFile profile);
    }
}
