using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTO
{
    public class AdminApprovalRequestDto
    {
        public Guid InstructorId { get; set; }
        public string InstructorName { get; set; }
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseImage { get; set; }
        public  decimal Price { get; set; }
        public List<string> Videos { get; set; }
        public List<string> Documents { get; set; }
        public string Category { get; set; }
    }
}
