using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Features.Followers.Command;
using UserService.Application.RepoInterfaces.FollowerRepo;
using UserService.Application.RepoInterfaces.ViewRepo;
using UserService.Application.ServiceInterfaces.UserViewServiceInterface;
using UserService.Domain.Entities;

namespace UserService.Application.Features.Followers.CommandHandler
{
    public class FollowUnfollowCommandHandler : IRequestHandler<FollowUnfollowCommand, string>
    {
        private readonly IFollowerRepo _followerRepo;
        private readonly ICurrentUserService _currentUserService;
        private readonly IInstructorViewRepo _instructorViewRepo;

        public FollowUnfollowCommandHandler(IFollowerRepo followerRepo, ICurrentUserService currentUserService, IInstructorViewRepo instructorViewRepo)
        {
            _followerRepo = followerRepo;
            _currentUserService = currentUserService;
            _instructorViewRepo = instructorViewRepo;
        }

        public async Task<string> Handle(FollowUnfollowCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _currentUserService.UserId;

                if (!Guid.TryParse(request.InstructorId, out Guid instructorGuid))
                {
                    throw new ArgumentException("Invalid InstructorId format.");
                }

                var instructorExists = await _instructorViewRepo.InstructorExists(instructorGuid);
                if (!instructorExists)
                {
                    throw new ArgumentException("Instructor not found.");
                }

                if (userId == instructorGuid)
                    throw new UnauthorizedAccessException("You cannot follow/unfollow yourself.");

                var follower = await _followerRepo.FindFollower(userId, instructorGuid);

                if (follower == null)
                {
                    var newFollower = new Follower
                    {
                        UserId = userId,
                        InstructorId = instructorGuid,
                        Created_by =  userId.ToString(),
                        Created_on = DateTime.UtcNow,
                        Updated_by = userId.ToString(),
                        Updated_on = DateTime.UtcNow,
                        Deleted_on = null,
                        Deleted_by = null

                    };

                    await _followerRepo.AddFollower(newFollower);
                    await _followerRepo.SaveChangesAsyncCustom();

                    return "Followed";
                }
                else if (follower.Is_deleted == true)
                {
                    follower.Updated_on = DateTime.UtcNow;
                    follower.Is_deleted = false;

                    await _followerRepo.SaveChangesAsyncCustom();

                    return "Followed";
                }
                else
                {
                    follower.Updated_on = DateTime.UtcNow;
                    follower.Is_deleted = true;

                    await _followerRepo.SaveChangesAsyncCustom();
                    return "Unfollowed";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
