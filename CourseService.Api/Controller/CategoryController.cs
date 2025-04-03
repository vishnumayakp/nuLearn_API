
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CourseService.Application.Common;
using CourseService.Application.DTO.CategoryDTO;
using CourseService.Application.Categories.Query;
using CourseService.Application.Categories.Command;
using Microsoft.AspNetCore.Authorization;



namespace CourseService.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all-categories")]

        public async Task<IActionResult> Getcategories()
        {
            try
            {
                var res = await _mediator.Send(new CategoriesViewQuery());
                if (res == null)
                {
                    return NotFound(new ApiResponse<List<CategoryDto>>(404, "Not Found", res, "categories Not Found"));
                }
                return Ok(new ApiResponse<List<CategoryDto>>(200, "Suucess", res, "Suuccessfully fetched Categories"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
            }

        }


        [HttpPost("add-category")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCategory([FromForm] CategoryAddCommandWithoutAdmin command)
        {
            try
            {
                var adminId = Convert.ToString(HttpContext.Items["UserId"]);

                if (!Guid.TryParse(adminId, out Guid adminGuidId))
                {
                    return Unauthorized(new ApiResponse<string>(401, "Unauthorized", null, "Admin ID not found in token"));
                }

                // Manually map the extracted AdminId
                var categoryCommand = new CategoryAddCommand(adminGuidId, command.Name, command.Image);

                var res = await _mediator.Send(categoryCommand);

                if (!res)
                {
                    return BadRequest(new ApiResponse<string>(404, "BadRequest", null, "Something went wrong"));
                }

                return Ok(new ApiResponse<string>(200, "Success", null, "Successfully Added Category"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
            }
        }

    }
}
