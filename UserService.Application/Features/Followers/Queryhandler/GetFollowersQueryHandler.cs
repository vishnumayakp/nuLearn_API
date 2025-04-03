using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTO.FollowerDto;
using UserService.Application.Features.Followers.Queries;
using UserService.Application.RepoInterfaces.FollowerRepo;
using UserService.Domain.Entities;

namespace UserService.Application.Features.Followers.Queryhandler
{
    public class GetFollowersQueryHandler:IRequestHandler<GetFollowersQuery,List<InstructorFollowerDto>>
    {
        private readonly IFollowerRepo _followerRepo;

        public GetFollowersQueryHandler(IFollowerRepo followerRepo)
        {
            _followerRepo = followerRepo;
        }

        public async Task<List<InstructorFollowerDto>> Handle(GetFollowersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var followers = await _followerRepo.GetFollowersByInstructorId(request.InstructorId);

                if(followers == null)
                {
                    return new List<InstructorFollowerDto>();
                }

                return followers.Select(a => new InstructorFollowerDto
                {
                    Id = a.Id,
                    Name = a.User.Name,
                    Profile = a.User.Profile
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
