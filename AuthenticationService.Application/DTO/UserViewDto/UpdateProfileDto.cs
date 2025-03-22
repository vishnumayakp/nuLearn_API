using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.DTO.UserViewDto
{
    public class UpdateProfileDto
    {
        public string Name { get; set; }
        public IFormFile Profile { get; set; }
    }
}
