using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Application.DTO.CategoryDTO
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
    }
}
