using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.DTO.InstructorAuthDto
{
    public class InstructorLoginResDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Tag { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? Profile { get; set; }
    }
}
