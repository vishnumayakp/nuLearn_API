using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTO.UserViewDto;
using UserService.Application.Features.Instructors.Queries;
using UserService.Application.Features.Users.Queries;
using UserService.Application.RepoInterfaces.ViewRepo;
using UserService.Domain.Entities;

namespace UserService.Application.Features.Users.QueryHandler
{
    public class GetUserProfileQueryHandler:IRequestHandler<GetUserProfileQuery, UserProfileDto>
    {
        private readonly IUserViewRepo _userViewRepo;

        public GetUserProfileQueryHandler(IUserViewRepo userViewRepo)
        {
            _userViewRepo = userViewRepo;
        }

        public async Task<UserProfileDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _userViewRepo.GetUserById(request.UserId);
            if (user == null)
            {
                throw new Exception("User profile not found.");
            }

            return new UserProfileDto
            {
                Name = user.Name,
                Email = user.Email,
                Profile = user.Profile
            };
        } 
    }

}
