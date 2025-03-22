using UserService.Application.Auth.UserAuth.Commands;
using UserService.Application.Auth.UserAuth.Commands.ForgotPw;
using UserService.Application.Auth.UserAuth.Commands.Login;

using UserService.Application.Common.ApiResponse;
using UserService.Application.DTO.UserAuthDto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Auth.UserAuth.Commands.Register;
using UserService.Application.Auth.UserAuth.Commands.VerifyOtp;
using UserService.Application.Auth.UserAuth.Commands.ResetPw;

namespace AuthenticationService.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserAuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromForm] UserRegisterDto userRegisterDto)
        {
            try
            {
                var command = new RegisterUserCommand(userRegisterDto.Name, userRegisterDto.Email, userRegisterDto.Password);
                var register= await _mediator.Send(command);
                if(register) return Ok(new ApiResponse<string>(200, "Success", "User Registered Successfully"));
                return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
            }
            catch (Exception ex)
            {
                 return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
            }
        }

        [HttpPost("verify-user")]

        public async Task<IActionResult> VerifyEmail([FromForm] VerifyOtpCommand verifyOtpCommand)
        {
            try
            {
                var res = await _mediator.Send(verifyOtpCommand);
                if (res)
                {
                    return Ok(new ApiResponse<string>(200, "Success", "Email Verified Successfully"));
                }
                return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
            }
        }

        [HttpPost("login")]

        public async Task<IActionResult> UserLogin([FromForm] UserLoginDto userLoginDto)
        {
            try
            {
                if (userLoginDto == null) return BadRequest(new ApiResponse<string>(400, "Failed", null, "User Login field is required"));
                var command = new LoginCommand(userLoginDto.Email, userLoginDto.Password);
                var res = await _mediator.Send(command);
                return Ok(new ApiResponse<UserLoginResDto>(200, "Success",res));        
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
            }
        }

        [HttpPost("forget-password")]

        public async Task<IActionResult> ForgetPassword([FromForm] ForgetPasswordCommand command)
        {
            try
            {
                var res = await _mediator.Send(command);
                if (res)
                {
                    return Ok(new ApiResponse<string>(200, "Success", "OTP sended for reset password"));
                }
                return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
            }
        }

        [HttpPost("verifyOtp-resetpassword")]

        public async Task<IActionResult> VerifyEmailForResetpassWord([FromForm] VerifyOtpResetPwCommand command)
        {
            try
            {
                var res = await _mediator.Send(command);
                if (res)
                {
                    return Ok(new ApiResponse<string>(200, "Success", "OTP verified for reset password"));
                }
                return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
            }
        }

        [HttpPost("reset-password")]

        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordCommand command)
        {
            try
            {
                var res = await _mediator.Send(command);
                if (res)
                {
                    return Ok(new ApiResponse<string>(200, "Success", "Password Successfully Changed"));
                }
                return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
            }
        }
    }
}
