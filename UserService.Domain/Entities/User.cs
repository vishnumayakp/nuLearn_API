using UserService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entities;

namespace UserService.Domain.Entity
{
    public class User:AuditableEntity
    {
        public Guid Id { get; set; }


        [Required(ErrorMessage = "Name is Required")]
        [StringLength(20, ErrorMessage = "Name cannot exceed 20 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
        public string? Password { get; set; }
        public string? Profile { get; set; }
        public bool Is_blocked { get; set; } = false;

        public virtual List<Follower> Followings { get; set; }

    }
}
