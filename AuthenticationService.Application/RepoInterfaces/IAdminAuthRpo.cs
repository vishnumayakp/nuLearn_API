using AuthenticationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.RepoInterfaces
{
    public interface IAdminAuthRpo
    {
        Task<Admin> GetAdminorByEmail(string email);
    }
}
