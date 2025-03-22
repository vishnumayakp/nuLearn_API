using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTO.UserViewDto;
using UserService.Application.Features.Users.Queries;
using UserService.Application.RepoInterfaces.ViewRepo;
using UserService.Domain.Entity;

namespace UserService.Application.Features.Users.QueryHandler
{
    public class UsersViewQueryHandler:IRequestHandler<UsersViewQuery,List<UserViewDto>>
    {
        private  readonly IUserViewRepo _userViewRepo;

        public UsersViewQueryHandler(IUserViewRepo userViewRepo)
        {
            _userViewRepo = userViewRepo;
        }

        public async Task<List<UserViewDto>> Handle(UsersViewQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _userViewRepo.GetAllUser();
                if (users == null)
                {
                    throw new Exception("Users Not Found");
                }

                var res = users.Select(a => new UserViewDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Email = a.Email,
                    Profile = a.Profile,
                    Is_Blocked = a.Is_blocked

                }).ToList();
                return res;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
