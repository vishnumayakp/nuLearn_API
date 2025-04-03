using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTO.InstructorViewDto;

namespace UserService.Application.Features.Instructors.Queries
{
    public class GetInstructorProfileQuery:IRequest<InstructorProfileDto>
    {
        public Guid InstructorId { get; set; }
    }
}
