using AuthenticationService.Application.AdminAuth.Commands;
using AuthenticationService.Application.Common.ApiResponse;
using AuthenticationService.Application.DTO.AdminAuthDto;
using AuthenticationService.Application.DTO.InstructorAuthDto;
using AuthenticationService.Application.InstructorAuth.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Api.Controller
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
                return Ok(new ApiResponse<string>(200, "Success", "Admin Login Successfully", res));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
            }
        }
    }
}
