using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Application.Categories.Command
{
    public record RequestAdminVerificationCommand(Guid Id, string CategoryName, string Image):IRequest<bool>;    
}
