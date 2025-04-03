using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTO.InstructorViewDto;
using UserService.Application.DTO.UserViewDto;
using UserService.Application.Features.Instructors.Commands;
using UserService.Application.Features.Users.Commands;
using UserService.Application.RepoInterfaces.ViewRepo;
using UserService.Application.ServiceInterfaces;

namespace UserService.Application.Features.Instructors.Handlers
{
    public class UpdateInstrProfileCommandHandler : IRequestHandler<UpdateInstrProfileCommand, UpdateProfileResponseDto>
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

        public async Task<UpdateProfileResponseDto> Handle(UpdateInstrProfileCommand request, CancellationToken cancellationToken)
        {
            var userId = Convert.ToString(_contextAccessor.HttpContext.Items["UserId"]);
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid userGuid))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var instructor = await _viewRepo.GetInstructorById(userGuid);
            if (instructor == null)
            {
                throw new KeyNotFoundException("Instructor profile not found.");
            }

            // Update only non-null values
            instructor.Name = request.UpdateProfileDto.Name ?? instructor.Name;
            instructor.Tag = request.UpdateProfileDto.Tag ?? instructor.Tag;
            instructor.Phone = request.UpdateProfileDto.Phone ?? instructor.Phone;
            instructor.Description = request.UpdateProfileDto.Description ?? instructor.Description;
            instructor.LinkedIn_Url = request.UpdateProfileDto.LinkedIn_Url ?? instructor.LinkedIn_Url;

            // Upload image only if provided
            if (request.UpdateProfileDto.Profile != null)
            {
                var imageUrl = await _cloudinaryService.UploadProfileImage(request.UpdateProfileDto.Profile);
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    instructor.Profile = imageUrl;
                }
            }

            var updateResult = await _viewRepo.SaveChangeAsyncCustom();
            if (!updateResult)
            {
                throw new Exception("Error updating instructor profile.");
            }

            // Return updated profile with image URL
            return new UpdateProfileResponseDto
            {
                Name = instructor.Name,
                Tag = instructor.Tag,
                Phone = instructor.Phone,
                Description = instructor.Description,
                LinkedIn_Url = instructor.LinkedIn_Url,
                Profile = instructor.Profile // This will be the image URL
            };
        }






    }
}
