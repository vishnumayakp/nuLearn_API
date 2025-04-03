using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Events.Request
{
    public class InstructorVerificationRequested
    {
        public Guid InstructorId { get; set; }

        public InstructorVerificationRequested(Guid instructorId)
        {
            InstructorId = instructorId;
        }
    }
}
