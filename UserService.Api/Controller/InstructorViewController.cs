using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sprache;
using UserService.Application.Common.ApiResponse;
using UserService.Application.DTO.InstructorDto;
using UserService.Application.DTO.InstructorViewDto;
using UserService.Application.DTO.UserViewDto;
using UserService.Application.Features.Instructors.Commands;
using UserService.Application.Features.Instructors.Queries;
using UserService.Application.Features.Users.Queries;

namespace UserService.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorViewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InstructorViewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("update-profile")]

        public async Task<IActionResult> UpdateInstructorProfile([FromForm] UpdateProfileRequestDto dto)
        {
            try
            {

                if (dto == null)
                {
                    return BadRequest("UpdateProfileDto is required.");
                }

                var command = new UpdateInstrProfileCommand { UpdateProfileDto = dto };
                var result = await _mediator.Send(command); 

                if (result==null)
                {
                    return BadRequest(new ApiResponse<UpdateProfileResponseDto>(404, "Profile Updation is Failed"));
                }

                return Ok(new ApiResponse<UpdateProfileResponseDto>(200, "Success", result, ""));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
            }
        }

        [HttpGet("instructor/id")]
        public async Task<IActionResult> GetInstructorsById(Guid id)
        {
            try
            {
                var userId = Convert.ToString(HttpContext.Items["UserId"]);

                if (Guid.TryParse(userId, out Guid userIdGuid))
                {
                    var res = await _mediator.Send(new InstructorViewQuery(id,userId));
                    if (res == null)
                    {
                        return NotFound(new ApiResponse<InstructorViewDto>(404, "Not Found", res, "Instructor Not Found"));
                    }

                    return Ok(new ApiResponse<InstructorViewDto>(200, "Success", res, "Successfully Fetched  Instructors"));
                }
                return Unauthorized(new ApiResponse<string>(401, "UnAuthorized", null, "User Not Authorised"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
            }
        }

        [HttpGet("all-instructor")]

        public async Task<IActionResult> GetAllInstructors()
        {
            try
            {
                var res = await _mediator.Send(new InstructorsViewQuery());
                if (res == null)
                {
                    return NotFound(new ApiResponse<List<InstructorViewDto>>(400, "Not Found", res, "Instructors Not Found"));
                }
                return Ok(new ApiResponse<List<InstructorViewDto>>(200, "Success", res, "Successfully Fetched all  Instructors"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
            }
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetInstructorProfile()
        {
            try
            {
                var userId = Convert.ToString(HttpContext.Items["UserId"]);

                if (Guid.TryParse(userId, out Guid userIdGuid))
                {
                    var query = new GetInstructorProfileQuery { InstructorId = userIdGuid };
                    var profile = await _mediator.Send(query);
                    return Ok(new ApiResponse<InstructorProfileDto>(200, "success", profile));
                }
                return Unauthorized(new ApiResponse<InstructorProfileDto>(401, "UnAuthorized", null, "Instructor Not Authorised"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
            }

        }
    }
}
