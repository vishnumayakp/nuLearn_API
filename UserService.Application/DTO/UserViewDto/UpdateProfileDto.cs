using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UserService.Application.DTO.UserViewDto
{
    public class UpdateProfileDto
    {
        public string? Name { get; set; }
        public string? Tag { get; set; }
        public string? Description { get; set; }
        public string? LinkedIn_Url { get; set; }
        public string? Phone { get; set; }
        public IFormFile? Profile { get; set; }
        [JsonIgnore]
        public string? ProfileUrl { get; set; }
    }

}
