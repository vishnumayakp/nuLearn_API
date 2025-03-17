using AuthenticationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.ServiceInterfaces
{
    public interface IJwtAdminService
    {
        string GenerateJwtToken(Admin admin);
    }
}
