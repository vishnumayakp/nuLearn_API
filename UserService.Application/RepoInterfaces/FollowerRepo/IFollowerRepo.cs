using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entities;

namespace UserService.Application.RepoInterfaces.FollowerRepo
{
    public interface IFollowerRepo
    {
        Task<bool> SaveChangesAsyncCustom();
        Task<Follower> FindFollower(Guid userId, Guid instructorId);
        Task<bool> AddFollower(Follower follower);
        Task<List<Follower>> GetFollowingByUserId(Guid userId);
        Task<List<Follower>> GetFollowersByInstructorId(Guid instructorId);
        Task<bool> IsFollowed(Guid instructorId, Guid userId);
    }
}
