using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Events.Response
{
    public class InstructorVerfied
    {
        public Guid InstructorId { get; set; }
        public bool IsVerified { get; set; }
        public string Message { get; set; }

        public InstructorVerfied(bool isVerified)
        {
            IsVerified = isVerified;
        }
    }

}
