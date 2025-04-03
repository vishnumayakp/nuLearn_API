using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Domain.Entities
{
    public class Video:AuditableEntity
    {
        [Key]
        public Guid VideoId { get; set; }

        [Required]
        public Guid CourseId { get; set; }

        [Required(ErrorMessage = "Video URL is required")]
        [Url(ErrorMessage = "Invalid Video URL format")]
        public string VideoUrl { get; set; }


        [Required(ErrorMessage = "Title is required")]
        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; }

    }
}
