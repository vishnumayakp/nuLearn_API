using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTO.FollowerDto;

namespace UserService.Application.Features.Followers.Queries
{
    public class GetFollowingQuery:IRequest<List<InstructorFollowingDto>>
    {
        public Guid UserId { get; set; }

        public GetFollowingQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
