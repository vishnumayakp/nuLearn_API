using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.ServiceInterfaces
{
    public interface IEmailService
    {
        Task SentEmail(string instructorName, string courseName, Guid courseId);
    }
}
