
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Events.Response
{
    public class CourseApproved
    {
        public Guid CourseId { get; set; }
        public bool IsApproved { get; set; }

        public CourseApproved() { }
        public CourseApproved( Guid courseId, bool isApproved)
        {
            CourseId = courseId;
            IsApproved = isApproved;
        }
    }
}
