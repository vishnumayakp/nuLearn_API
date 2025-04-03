using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTO.InstructorDto;
using UserService.Application.Features.Instructors.Queries;
using UserService.Application.RepoInterfaces.ViewRepo;
using UserService.Domain.Entities;

namespace UserService.Application.Features.Instructors.QueryHandlers
{
    public class InstructorsViewQueryHandler:IRequestHandler<InstructorsViewQuery,List<InstructorViewDto>>
    {
        private readonly IInstructorViewRepo _instructorViewRepo;
        public InstructorsViewQueryHandler(IInstructorViewRepo instructorViewRepo)
        {
            _instructorViewRepo = instructorViewRepo;
        }   

        public async Task<List<InstructorViewDto>> Handle(InstructorsViewQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var instructors = await _instructorViewRepo.GetAllInstructor();
                if(instructors == null)
                {
                    throw new Exception("Instructors Not Found");
                }

                var res = instructors.Select(i => new InstructorViewDto
                {
                    Id = i.Instructor_Id,
                    Name = i.Name,
                    Email = i.Email,
                    Tag = i.Tag,
                    Description = i.Description,
                    Profile = i.Profile,
                    Phone = i.Phone,
                }).ToList();

                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
