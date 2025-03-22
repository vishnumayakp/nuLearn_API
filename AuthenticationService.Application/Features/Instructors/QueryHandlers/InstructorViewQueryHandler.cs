using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTO.InstructorDto;
using UserService.Application.Features.Instructors.Commands;
using UserService.Application.Features.Instructors.Queries;
using UserService.Application.RepoInterfaces.ViewRepo;

namespace UserService.Application.Features.Instructors.QueryHandlers
{
    public class InstructorViewQueryHandler:IRequestHandler<InstructorViewQuery, InstructorViewDto>
    {
        private readonly IInstructorViewRepo _instructorViewRepo;

        public InstructorViewQueryHandler(IInstructorViewRepo instructorViewRepo)
        {
            _instructorViewRepo = instructorViewRepo;
        }

        public async Task<InstructorViewDto> Handle(InstructorViewQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var instructor = await _instructorViewRepo.GetInstructorById(request.Id);

                if (instructor == null)
                {
                    throw new Exception("Instructor Not Found");
                }
                      
                var res = new InstructorViewDto
                {
                    Id = instructor.Instructor_Id,
                    Name = instructor.Name,
                    Email=instructor.Email,
                    Tag = instructor.Tag,
                    Description = instructor.Description,
                    Profile = instructor.Profile,
                    Phone = instructor.Phone,
                };
               
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
