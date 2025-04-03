using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.ServiceInterfaces.UserViewServiceInterface;

namespace UserService.Infrastructure.Services.UserViewService
{
    public class CurrentUserService:ICurrentUserService
    {
        public Guid UserId { get; set; }

        public CurrentUserService(IHttpContextAccessor _httpContextAccessor)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userIdClaim) && Guid.TryParse(userIdClaim, out Guid parsedUserId))
            {
                UserId = parsedUserId;
            }
            else
            {
                throw new UnauthorizedAccessException("User ID not found in token.");
            }
        }
    }
}
