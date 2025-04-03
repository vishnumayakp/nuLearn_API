using UserService.Domain.Entities;
using UserService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.ServiceInterfaces.AuthServiceInterface
{
    public interface IJwtInstrService
    {
        string GenerateJwtToken(Instructor instructor);
    }
}
