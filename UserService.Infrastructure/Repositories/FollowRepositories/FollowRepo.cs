using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.RepoInterfaces.FollowerRepo;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Repositories.FollowRepositories
{
    public class FollowRepo:IFollowerRepo
    {
        private readonly AppDbContext _appDbContext;

        public FollowRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> SaveChangesAsyncCustom()
        {
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Follower> FindFollower(Guid userId, Guid instructorId)
        {
            var follower = await _appDbContext.Followers.FirstOrDefaultAsync(f => f.UserId == userId && f.InstructorId == instructorId);
            return follower;
        }

        public async Task<bool> AddFollower(Follower follower)
        {
            await _appDbContext.Followers.AddAsync(follower);
            return true;
        }

        public async Task<List<Follower>> GetFollowingByUserId(Guid userId)
        {
            var followings = await _appDbContext.Followers.Include(i=>i.Instructor).Where(u=>u.UserId == userId && u.Is_deleted==false)
                .ToListAsync();
            return followings;
        }

        public async Task<List<Follower>> GetFollowersByInstructorId(Guid instructorId)
        {
            var followers = await _appDbContext.Followers.Include(u=>u.User).
                Where(i=>i.InstructorId==instructorId && i.Is_deleted==false)
                .ToListAsync();
            return followers;
        }

        public async Task<bool> IsFollowed(Guid instructorId, Guid userId)
        {
            var follower = await _appDbContext.Followers
                .FirstOrDefaultAsync(
                i => i.InstructorId == instructorId &&
                i.UserId == userId &&
                i.Is_deleted == false);
            if (follower == null) return false;
            return true;
        }
    }
}
