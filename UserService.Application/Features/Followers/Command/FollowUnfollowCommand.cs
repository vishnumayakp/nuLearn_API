using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UserService.Application.Features.Followers.Command
{
    public class FollowUnfollowCommand : IRequest<string>
    {
        public string InstructorId { get; set; }
    }
}
