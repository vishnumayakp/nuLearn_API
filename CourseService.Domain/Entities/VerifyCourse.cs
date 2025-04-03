using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Domain.Entities
{
    public class VerifyCourse:AuditableEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid InstructorId { get; set; }

        [Required]
        public Guid CategoryId { get; set; }


        [Required(ErrorMessage = "Course Name is required")]
        [MaxLength(100)]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public CourseType Type { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        public string ImageUrl { get; set; }

        [Range(0, 100000)]
        public decimal MRP { get; set; }

        [Range(0, 100000)]
        public decimal Price { get; set; }
        public bool IsApproved { get; set; } = false;
        public List<string> VideoUrls { get; set; }
        public List<string> DocumentUrls { get; set; }
    }
}
