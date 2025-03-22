using UserService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entities;

namespace UserService.Application.RepoInterfaces.AuthRepo
{
    public interface IAdminAuthRpo
    {
        Task<Admin> GetAdminorByEmail(string email);
    }
}
