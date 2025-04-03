using CourseService.Application.Common;
using CourseService.Application.Courses.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-course")]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> AddCourse([FromForm] AddCourseRequest command)
        {
            try
            {
                var instructorId = Convert.ToString(HttpContext.Items["UserId"]);

                if (!Guid.TryParse(instructorId, out Guid instGuidId))
                {
                    return Unauthorized(new ApiResponse<string>(401, "Unauthorized", null, "Admin ID not found in token"));
                }

                var courseCommand = new AddCourseCommand(instGuidId,
                        command.CategoryId,
                        command.CourseName,
                        command.Description,
                        command.Type,
                        command.ImageUrl,
                        command.MRP,
                        command.Price,
                        command.Videos,
                        command.Documents);

                var res = await _mediator.Send(courseCommand);
                if (res == null)
                {
                    return BadRequest(new ApiResponse<string>(404, "BadRequest", null, "Something went wrong"));
                }
                return Ok(new ApiResponse<string>(200, "Success", null, "Successfully Added Your Course"));

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(400, "Something went wrong", null, ex.Message));
            }
        }

    }
}
