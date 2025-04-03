using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTO.UserViewDto;
using UserService.Application.Features.Users.Commands;
using UserService.Application.RepoInterfaces.ViewRepo;
using UserService.Application.ServiceInterfaces;
using UserService.Application.ServiceInterfaces.UserViewServiceInterface;

namespace UserService.Application.Features.Users.Handlers
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, bool>
    {
        private readonly IUserViewRepo _userViewRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICloudinaryService _cloudinaryService;

        public UpdateProfileCommandHandler(IUserViewRepo userViewRepo,IHttpContextAccessor httpContextAccessor, ICloudinaryService cloudinaryService)
        {
            _userViewRepo = userViewRepo;
            _httpContextAccessor = httpContextAccessor;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<bool> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = Convert.ToString(_httpContextAccessor.HttpContext?.Items["UserId"]);
                if(!Guid.TryParse(userId, out Guid userGuid))
                {
                    throw new UnauthorizedAccessException("UserId is not valid.");
                }

                var user = await _userViewRepo.GetUserById(userGuid);
                if (user == null)
                {
                    throw new KeyNotFoundException("User not found.");
                }
                if (!string.IsNullOrEmpty(request.Name)) user.Name = request.Name;

                if (request.Profile != null)
                {
                    user.Profile = await _cloudinaryService.UploadProfileImage(request.Profile);
                }

                await _userViewRepo.UpdateUserAsyn(user);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
    }
}
