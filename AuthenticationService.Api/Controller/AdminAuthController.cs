using UserService.Application.Common.ApiResponse;
using UserService.Application.DTO.AdminAuthDto;
using UserService.Application.DTO.InstructorAuthDto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Auth.AdminAuth.Commands.Login;

namespace UserService.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminAuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminAuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]

        public async Task<IActionResult> AdminLogin([FromBody] AdminLoginDto loginDto)
        {
            try
            {
                var command = new AdminLoginCommand(loginDto.Email, loginDto.Password);
                var res = await _mediator.Send(command);
                return Ok(new ApiResponse<string>(200, "Success", res));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
            }
        }
    }
}
