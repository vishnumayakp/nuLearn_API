using AuthenticationService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.ServiceInterfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(User user);
    }
}
