using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTO.FollowerDto;

namespace UserService.Application.Features.Followers.Queries
{
    public class GetFollowersQuery:IRequest<List<InstructorFollowerDto>>
    {
        public Guid InstructorId { get; set; }

        public GetFollowersQuery(Guid instructorId)
        {
            InstructorId = instructorId;
        }
    }
}
