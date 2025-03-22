using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Entities
{
    public class Instructor:AuditableEntity
    {
        [Key]
        public Guid Instructor_Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [StringLength(20, ErrorMessage = "Name cannot exceed 20 characters.")]
        public string? Name { get; set; }
        public string? Tag { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? Profile { get; set; }
        public bool Is_Blocked { get; set; }
        public string? Description { get; set; }
        public string? LinkedIn_Url { get; set; }
        public string? Certificate_Url { get; set; }

    }
}
