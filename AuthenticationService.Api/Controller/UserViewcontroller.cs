using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Common.ApiResponse;
using UserService.Application.DTO.InstructorDto;
using UserService.Application.DTO.UserViewDto;
using UserService.Application.Features.Users.Commands;
using UserService.Application.Features.Users.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UserService.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserViewcontroller : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserViewcontroller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("profile-updation")]

        public async Task<IActionResult> UpdateProfileUser([FromForm] UpdateProfileCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                if (!result)
                {
                    return BadRequest(new ApiResponse<string>(200, "Profile Updation is Failed"));
                }

                return Ok(new ApiResponse<UpdateProfileDto>(200, "Success",null,"Profile Updated Success fully"));
                
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
            }
        }

        [HttpGet("user/id")]

        public async Task<IActionResult> GetAllUsers(Guid Id)
        {
            try
            {
                var res = await _mediator.Send(new UserViewQuery(Id));

                if(res == null)
                {
                    return NotFound(new ApiResponse<UserViewDto>(400, "Not Found", res, "User Not Found"));
                }
                return Ok(new ApiResponse<UserViewDto>(200, "Success", res, "Successfully Fetched  User"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
            }
        }

        [HttpGet("all-user")]

        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var res = await _mediator.Send(new UsersViewQuery());
                if(res == null)
                {
                    return NotFound(new ApiResponse<List<UserViewDto>>(400, "Not Found", res, "Users Not Found"));
                }
                return Ok(new ApiResponse<List<UserViewDto>>(200, "Success", res, "Successfully Fetched all  User"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
            }
        }
    }
}
