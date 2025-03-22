
using UserService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.ServiceInterfaces.AuthServiceInterface
{
    public interface IJwtAdminService
    {
        string GenerateJwtToken(Admin admin);
    }
}
