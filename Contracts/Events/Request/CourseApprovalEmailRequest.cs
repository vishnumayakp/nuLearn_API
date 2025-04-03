using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Events.Request
{
    public record CourseApprovalEmailRequest(Guid InstructorId,string InstructorName, string CourseName, string CourseImage);
}
