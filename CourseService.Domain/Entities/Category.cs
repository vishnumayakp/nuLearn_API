using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Domain.Entities
{
    public class Category:AuditableEntity
    {
        public Guid Id { get; set; }


        [Required(ErrorMessage = "Category Name is required")]
        [MaxLength(100, ErrorMessage = "Category Name cannot exceed 100 characters")]
        public string Category_Name { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        public string Image { get; set; }

        public List<Course> Courses { get; set; } = new();
    }
}
