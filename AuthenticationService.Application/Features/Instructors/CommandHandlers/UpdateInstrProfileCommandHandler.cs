using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Features.Instructors.Commands;
using UserService.Application.RepoInterfaces.ViewRepo;
using UserService.Application.ServiceInterfaces;

namespace UserService.Application.Features.Instructors.Handlers
{
    public class UpdateInstrProfileCommandHandler:IRequestHandler<UpdateInstrProfileCommand, bool>
    {
        private readonly IInstructorViewRepo _viewRepo;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICloudinaryService _cloudinaryService;

        public UpdateInstrProfileCommandHandler(IInstructorViewRepo viewRepo, IHttpContextAccessor contextAccessor, ICloudinaryService cloudinaryService)
        {
            _viewRepo = viewRepo;
            _contextAccessor = contextAccessor;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<bool> Handle(UpdateInstrProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = Convert.ToString(_contextAccessor.HttpContext.Items["UserId"]);
                if(!Guid.TryParse(userId, out Guid userGuid))
                {
                    throw new UnauthorizedAccessException("UserId is not valid.");
                }

                var user = await _viewRepo.GetInstructorById(userGuid);
                if(user == null)
                {
                    throw new UnauthorizedAccessException("UserId  not Found.");
                }

                if(!string.IsNullOrEmpty(request.Name)) user.Name = request.Name;
                if(!string.IsNullOrEmpty(request.Tag)) user.Tag = request.Tag;
                if(!string.IsNullOrEmpty(request.Description)) user.Description = request.Description;
                if(!string.IsNullOrEmpty(request.LinkedIn_Url)) user.LinkedIn_Url = request.LinkedIn_Url;
                if (!string.IsNullOrEmpty(request.Phone)) user.Phone = request.Phone;
                if (request.Profile!=null)
                {
                   user.Profile = await _cloudinaryService.UploadProfileImage(request.Profile);
                }

                await _viewRepo.UpdateUserAsyn(user);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
