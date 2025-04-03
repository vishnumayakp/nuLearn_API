using CourseService.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Application.DTO.CourseDTO
{
    public class AddCourseDto
    {
        public Guid InstructorId { get; set; }
        public Guid CategoryId { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public CourseType Type { get; set; }
        public IFormFile Image { get; set; }
        public decimal MRP { get; set; }
        public decimal Price { get; set; }
        public List<IFormFile> Videos { get; set; }
        public List<IFormFile> Documents { get; set; }
    }
}
