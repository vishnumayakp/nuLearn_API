using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTO.FollowerDto;
using UserService.Application.Features.Followers.Queries;
using UserService.Application.RepoInterfaces.FollowerRepo;

namespace UserService.Application.Features.Followers.Queryhandler
{
    public class GetFollowingQueryHandler:IRequestHandler<GetFollowingQuery, List<InstructorFollowingDto>>
    {
        private readonly IFollowerRepo _followerRepo;

        public GetFollowingQueryHandler(IFollowerRepo followerRepo)
        {
            _followerRepo = followerRepo;
        }

        public async Task<List<InstructorFollowingDto>> Handle(GetFollowingQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var followings = await _followerRepo.GetFollowingByUserId(request.UserId);

                if(followings == null)
                {
                    return new List<InstructorFollowingDto>();
                }

                return followings.Select(f => new InstructorFollowingDto
                {
                    Id = f.Id,
                    Name = f.Instructor.Name,
                    Tag = f.Instructor.Tag,
                    Profile = f.Instructor.Profile
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
