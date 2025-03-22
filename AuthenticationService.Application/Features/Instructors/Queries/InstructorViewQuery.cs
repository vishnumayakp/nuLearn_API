using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTO.InstructorDto;

namespace UserService.Application.Features.Instructors.Queries
{
    public class InstructorViewQuery:IRequest<InstructorViewDto>
    {
        public Guid Id { get; set; }

        public InstructorViewQuery(Guid id)
        {
            Id = id;
        }
    }
}
