using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTO.InstructorDto;
using UserService.Application.Features.Instructors.Commands;
using UserService.Application.Features.Instructors.Queries;
using UserService.Application.RepoInterfaces.FollowerRepo;
using UserService.Application.RepoInterfaces.ViewRepo;

namespace UserService.Application.Features.Instructors.QueryHandlers
{
    public class InstructorViewQueryHandler:IRequestHandler<InstructorViewQuery, InstructorViewDto>
    {
        private readonly IInstructorViewRepo _instructorViewRepo;
        private readonly IFollowerRepo _followerRepo;

        public InstructorViewQueryHandler(IInstructorViewRepo instructorViewRepo,IFollowerRepo followerRepo)
        {
            _instructorViewRepo = instructorViewRepo;
            _followerRepo = followerRepo;
        }

        public async Task<InstructorViewDto> Handle(InstructorViewQuery request, CancellationToken cancellationToken)
        {
            try
            {
                bool? isFollowed = null;

                if (!string.IsNullOrEmpty(request.UserId)) // Ensure UserId is not null or empty
                {
                    Guid userGuid;
                    if (Guid.TryParse(request.UserId, out userGuid)) // Convert string to Guid safely
                    {
                        isFollowed = await _followerRepo.IsFollowed(request.Id, userGuid);
                    }
                }

                var instructor = await _instructorViewRepo.GetInstructorById(request.Id);

                if (instructor == null)
                {
                    throw new Exception("Instructor Not Found");
                }

                var res = new InstructorViewDto
                {
                    Id = instructor.Instructor_Id,
                    Name = instructor.Name,
                    Email = instructor.Email,
                    Tag = instructor.Tag,
                    Description = instructor.Description,
                    Profile = instructor.Profile,
                    Phone = instructor.Phone,
                    Is_Blocked = instructor.Is_Blocked,
                    Is_Followed = isFollowed ?? false
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
