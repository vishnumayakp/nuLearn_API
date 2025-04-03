using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entities;

namespace UserService.Application.RepoInterfaces.ViewRepo
{
    public interface IAdminViewRepo
    {
        Task<Admin> GetAdminById(Guid id);
        Task<string> GetAdminRole(Guid adminId);
        Task<bool> AdminExists(Guid adminId);
    }
}
