using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entity;

namespace UserService.Domain.Entities
{
    public class Follower:AuditableEntity
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "UserId is Required")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "InstructorId is Required")]
        public Guid InstructorId { get; set; }

        public virtual User User  { get; set; }
        public virtual  Instructor Instructor { get; set; }
    }
}
