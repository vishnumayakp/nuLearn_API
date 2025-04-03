using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.DTO.UserViewDto
{
    public class UserViewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Profile { get; set; }
        public string Email { get; set; }
        public bool Is_Blocked { get; set; }

    }
}
