
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Common.ApiResponse;
using UserService.Application.DTO.FollowerDto;
using UserService.Application.DTO.InstructorDto;
using UserService.Application.DTO.UserViewDto;
using UserService.Application.Features.Followers.Command;
using UserService.Application.Features.Followers.Queries;
using UserService.Application.RepoInterfaces.FollowerRepo;
using UserService.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UserService.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FollowerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("follow-unfollow")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> FollowUnfollow([FromBody] FollowUnfollowCommand command)
        {
            try
            {
                var userId = Convert.ToString(HttpContext.Items["UserId"]);

                if (Guid.TryParse(userId, out Guid userIdGuid))
                {
 
                    if (command == null)
                    {
                        return BadRequest(new ApiResponse<string>(400, "BadRequest", "Instructor ID cannot be empty."));
                    }

                    Console.WriteLine($"Received InstructorId: {command.InstructorId}");

                    var result = await _mediator.Send(command);

                    if (result == "Followed")
                    {
                        return Ok(new ApiResponse<string>(200, "Success", "Followed Successfully"));
                    }
                    else if (result == "Unfollowed")
                    {
                        return Ok(new ApiResponse<string>(200, "Success", "Unfollowed Successfully"));
                    }
                    else
                    {
                        return BadRequest(new ApiResponse<string>(400, "Error", "Invalid operation"));
                    }
                }

                return Unauthorized(new ApiResponse<string>(401, "Unauthorized", null, "Invalid user ID in token"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(500, "Error", null, ex.Message));
            }
        }

        [HttpGet("get-followers")]

        public async Task<IActionResult> GetFollowers()
        {
            try
            {
                var instructorId = Convert.ToString(HttpContext.Items["UserId"]);

                if(Guid.TryParse(instructorId , out Guid instructorGuidId))
                {
                    var res = await _mediator.Send(new GetFollowersQuery(instructorGuidId));
                    if(res == null)
                    {
                        return NotFound(new ApiResponse<List<InstructorFollowerDto>>(400, "Not Found", res, "Followers Not Found"));
                    }
                    return Ok(new ApiResponse<List<InstructorFollowerDto>>(200, "Success",res, "Fetched Followers Successfully"));
                }
                return Unauthorized(new ApiResponse<string>(401, "Unauthorized", null, "Invalid user ID in token"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(500, "Error", null, ex.Message));
            }
        }

        [HttpGet("get-followings")]

        public async Task<IActionResult> GetFollowings()
        {
            try
            {
                var userId = Convert.ToString(HttpContext.Items["UserId"]);

                if (Guid.TryParse(userId, out Guid userGuidId))
                {
                    var res = await _mediator.Send(new GetFollowingQuery(userGuidId));

                    if(res == null)
                    {
                        return NotFound(new ApiResponse<List<InstructorFollowingDto>>(400, "Not Found", res, "Following Instructors Not Found"));
                    }
                    return Ok(new ApiResponse<List<InstructorFollowingDto>>(200, "Success", res, "Fetched Following Instructors Successfully"));
                }
                return Unauthorized(new ApiResponse<string>(401, "Unauthorized", null, "Invalid user ID in token"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(500, "Error", null, ex.Message));
            }
        }


    }
}
