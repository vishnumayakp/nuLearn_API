using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sprache;
using UserService.Application.Common.ApiResponse;
using UserService.Application.DTO.InstructorDto;
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

        [HttpPatch]

        public async Task<IActionResult> UpdateInstructorProfile([FromForm] UpdateInstrProfileCommand command)
        {
            try
            {
                var res = await _mediator.Send(command);
                if (!res)
                {
                    return BadRequest(new ApiResponse<string>(200, "Profile Updation is Failed"));
                }

                return Ok(new ApiResponse<UpdateProfileDto>(200, "Success", null, "Profile Updated Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
            }
        }

        [HttpGet("instructor/id")]
        public async Task<IActionResult> GetAllInstructors(Guid id)
        {
            try
            {
                var res = await _mediator.Send(new InstructorViewQuery(id));
                if(res == null)
                {
                    return NotFound(new ApiResponse<InstructorViewDto>(400,"Not Found", res, "Instructor Not Found"));
                }

                return Ok(new ApiResponse<InstructorViewDto>(200, "Success", res, "Successfully Fetched  Instructors"));
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
    }
}
