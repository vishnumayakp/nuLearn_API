using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Entities
{
    public class Admin:AuditableEntity
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
}
