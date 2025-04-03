using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTO.InstructorViewDto;
using UserService.Application.Features.Instructors.Queries;
using UserService.Application.RepoInterfaces.ViewRepo;

namespace UserService.Application.Features.Instructors.QueryHandlers
{
    public class GetInstructorProfileQueryHandler : IRequestHandler<GetInstructorProfileQuery, InstructorProfileDto>
    {
        private readonly IInstructorViewRepo _instructorRepo;

        public GetInstructorProfileQueryHandler(IInstructorViewRepo instructorRepo)
        {
            _instructorRepo = instructorRepo;
        }

        public async Task<InstructorProfileDto> Handle(GetInstructorProfileQuery request, CancellationToken cancellationToken)
        {
            var instructor = await _instructorRepo.GetInstructorById(request.InstructorId);

            if (instructor == null)
            {
                throw new Exception("Instructor profile not found.");
            }

            return new InstructorProfileDto
            {
                Name = instructor.Name,
                Tag = instructor.Tag,
                Email = instructor.Email,
                Profile = instructor.Profile,
                Description = instructor.Description,
                LinkedIn_Url = instructor.LinkedIn_Url,
                Phone = instructor.Phone,
            };
        }
    }
}
