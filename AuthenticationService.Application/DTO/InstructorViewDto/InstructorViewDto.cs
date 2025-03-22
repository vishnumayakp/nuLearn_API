using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.DTO.InstructorDto
{
    public class InstructorViewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tag { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string LinkedIn_Url { get; set; }
        public string Profile { get; set; }
        public bool Is_Blocked { get; set; }

    }
}
