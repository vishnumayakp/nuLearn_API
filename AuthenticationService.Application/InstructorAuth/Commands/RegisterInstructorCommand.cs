using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.InstructorAuth.Commands
{
    public class RegisterInstructorCommand:IRequest<bool>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile UploadFile { get; set; }

        public RegisterInstructorCommand(string name, string email, string password, IFormFile uploadFile)
        {
            Name = name;
            Email = email;
            Password = password;
            UploadFile = uploadFile;
        }
    }
}
