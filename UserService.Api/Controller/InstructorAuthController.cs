﻿using UserService.Application.Auth.InstructorAuth.Commands;
using UserService.Application.Auth.InstructorAuth.Commands.ForgotPw;
using UserService.Application.Auth.InstructorAuth.Commands.Login;
using UserService.Application.Auth.InstructorAuth.Commands.Register;
using UserService.Application.Auth.InstructorAuth.Commands.ResetPw;
using UserService.Application.Auth.InstructorAuth.Commands.VerifyOtp;
using UserService.Application.Common.ApiResponse;
using UserService.Application.DTO.InstructorAuthDto;
using UserService.Application.DTO.UserAuthDto;
using UserService.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Auth.InstructorAuth.Commands.VerifyOtp;

namespace AuthenticationService.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorAuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InstructorAuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        [Consumes("multipart/form-data")]

        public async Task<IActionResult> Register([FromForm] InstructorRegisterDto instructorAuthDto)
        {
            try
            {

                var command = new RegisterInstructorCommand(instructorAuthDto.Name, instructorAuthDto.Email, instructorAuthDto.Password, instructorAuthDto.file);
                var register = await _mediator.Send(command);

                if (register)
                {
                    return Ok(new ApiResponse<string>(200, "Success", "Instructor Registered Successfully"));
                }
                return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
            }
        }

        [HttpPost("verify-user")]

        public async Task<IActionResult> VerifyEmail([FromBody] VerifyInstrOtpCommand verifyOtpCommand)
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

        public async Task<IActionResult> InstructorLogin([FromBody] InstructorLoginDto loginDto)
        {
            try
            {
                if(loginDto == null)
                {
                    return BadRequest(new ApiResponse<string>(400, "Failed", null, "User Login field is required"));
                }
                var command = new InstructorLoginCommand(loginDto.Email, loginDto.Password);
                var res = await _mediator.Send(command);
                return Ok(new ApiResponse<InstructorLoginResDto>(200, "Success",res));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
            }
        }

        [HttpPost("forget-password")]

        public async Task<IActionResult> ForgetPassword([FromBody] ForgotPwCommand command)
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

        public async Task<IActionResult> VerifyEmailForResetpassWord([FromBody] VerifyOtpResetPasswordCommand command)
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

        public async Task<IActionResult> ResetPassword([FromBody] ResetPwCommand command)
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
