using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTO.UserViewDto;
using UserService.Application.Features.Users.Queries;
using UserService.Application.RepoInterfaces.ViewRepo;

namespace UserService.Application.Features.Users.QueryHandler
{
    public class UserViewQueryHandler:IRequestHandler<UserViewQuery, UserViewDto>
    {
        private readonly IUserViewRepo _userViewRepo;

        public UserViewQueryHandler(IUserViewRepo userViewRepo)
        {
            _userViewRepo = userViewRepo;
        }

        public async Task<UserViewDto> Handle(UserViewQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userViewRepo.GetUserById(request.Id);
                if (user == null)
                {
                    throw new Exception("User Not Found");
                }

                var res = new UserViewDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Profile = user.Profile,
                    Is_Blocked = user.Is_blocked
                };
                return res;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
