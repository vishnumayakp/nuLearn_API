using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.ServiceInterfaces.UserViewServiceInterface
{
    public interface ICurrentUserService
    {
         Guid UserId { get; set; }
    }
}
