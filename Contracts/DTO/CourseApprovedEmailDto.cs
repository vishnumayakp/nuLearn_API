using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTO
{
    public class CourseApprovedEmailDto
    {
        public Guid InstructorId { get; set; }
        public string InstructorName { get; set; }
        public string InstructorEmail { get; set; }
        public string CourseName { get; set; }
        public string Image { get; set; }
        public string Message { get; set; }
    }
}
