using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTO.InstructorAuthDto;
using UserService.Application.DTO.InstructorViewDto;
using UserService.Application.DTO.UserViewDto;

namespace UserService.Application.Features.Instructors.Commands
{
    public class UpdateInstrProfileCommand : IRequest<UpdateProfileResponseDto>
    {
        public UpdateProfileRequestDto UpdateProfileDto { get; set; }

        public UpdateInstrProfileCommand() { }

        public UpdateInstrProfileCommand(UpdateProfileRequestDto updateProfileDto)
        {
            UpdateProfileDto = updateProfileDto;
        }
    }

}
