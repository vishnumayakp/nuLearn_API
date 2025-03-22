using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTO.UserViewDto;

namespace UserService.Application.Features.Users.Commands
{
    public class UpdateProfileCommand:IRequest<bool>
    {
        public string? Name { get; set; }
        public IFormFile? Profile { get; set; }

        public UpdateProfileCommand()
        {
           
        }
    }
}
