using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Features.Instructors.Commands
{
    public class UpdateInstrProfileCommand:IRequest<bool>
    {
        public string? Name { get; set; }
        public string? Tag { get; set; }
        public IFormFile? Profile { get; set; }
        public string? Description { get; set; }
        public string? LinkedIn_Url { get; set; }
        public string? Phone { get; set; }

        public UpdateInstrProfileCommand() { }

    }
}
