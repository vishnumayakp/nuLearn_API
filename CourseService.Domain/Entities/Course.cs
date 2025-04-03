using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Domain.Entities
{
    public class Course:AuditableEntity
    {
        [Key]
        public Guid Course_Id { get; set; }

        [Required(ErrorMessage = "Instructor Id is required")]
        public Guid Instructor_Id { get; set; }

        [Required(ErrorMessage = "Category Id is required")]
        public Guid Category_Id { get; set; }

        [Required(ErrorMessage = "Course Name is required")]
        [MaxLength(100, ErrorMessage = "Course Name cannot exceed 100 characters")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [Required]
        public CourseType Type { get; set; }


        [Required(ErrorMessage = "Image URL is required")]
        public string ImageUrl { get; set; }

        [Range(0, 100000, ErrorMessage = "MRP must be between 0 and 100000")]
        public decimal MRP { get; set; }

        [Range(0, 100000, ErrorMessage = "Price must be between 0 and 100000")]
        public decimal Price { get; set; }
        public bool IsBolcked { get; set; } = false;


        public ICollection<Video> VideoUrls { get; set; } = new List<Video>();
        public ICollection<Document> DocumentUrls { get; set; } = new List<Document>();
        public Category Category { get; set; }
    }

    public enum CourseType
    {
        Free,
        Paid
    }
}
