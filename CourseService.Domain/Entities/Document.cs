using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Domain.Entities
{
    public class Document:AuditableEntity
    {
        [Key]
        public Guid DocumentId { get; set; }

        [Required]
        public Guid CourseId { get; set; }

        [Required(ErrorMessage = "Document URL is required")]
        [Url(ErrorMessage = "Invalid Document URL format")]
        public string DocumentUrl { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; }


    }
}
