using Contracts.Events.Request;
using Contracts.Events.Response;
using CourseService.Application.RepoInterface.CourseRepoInterface;
using CourseService.Domain.Entities;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CourseService.Application.Consumers
{
    public class CourseApprovalConsumer: IConsumer<CourseApproved>
    {
        private readonly ICourseRepo _courseRepo;
        private readonly IPublishEndpoint _publishEndpoint;
        public CourseApprovalConsumer(ICourseRepo courseRepo, IPublishEndpoint publishEndpoint)
        {
            _courseRepo = courseRepo;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<CourseApproved> context)
        {
            var verfiCourse = await _courseRepo.GetVerifyCourseById(context.Message.CourseId);

            if (verfiCourse == null)
            {
                throw new Exception("Course Not Found in Verification Queue");
            }

            if(context.Message.IsApproved)
            {
                var course = new Course
                {
                    Course_Id = verfiCourse.Id,
                    Instructor_Id = verfiCourse.InstructorId,
                    Category_Id = verfiCourse.CategoryId,
                    CourseName = verfiCourse.CourseName,
                    Description = verfiCourse.Description,
                    Type = verfiCourse.Type,
                    ImageUrl = verfiCourse.ImageUrl,
                    MRP = verfiCourse.MRP,
                    Price = verfiCourse.Price,
                    Created_by = "Initial Create",
                    Created_on = DateTime.UtcNow,
                    Updated_by = "Initial Create",
                    Updated_on = DateTime.UtcNow,
                    Deleted_by = "Initial Create",
                    Deleted_on = DateTime.UtcNow,


                    VideoUrls = verfiCourse.VideoUrls.Select(v=> new Video
                    {
                        VideoId = Guid.NewGuid(),
                        CourseId = verfiCourse.Id,
                        VideoUrl = v,
                    }).ToList(),

                    DocumentUrls = verfiCourse.DocumentUrls.Select(d=> new Document
                    {
                        DocumentId = verfiCourse.Id,
                        CourseId= verfiCourse.Id,
                        DocumentUrl = d,
                    }).ToList()
                };
 
                await _courseRepo.AddCourse(course);

                verfiCourse.Is_deleted = true;
                verfiCourse.Deleted_by = "Admin Approval";
                verfiCourse.Deleted_on = DateTime.UtcNow;

                await _courseRepo.RemoveVerifyCourse(verfiCourse);
                await _courseRepo.SaveChangeAsyncCustom();

                await _publishEndpoint.Publish(new CourseApproved(course.Course_Id, true));
            }
            else
            {

                verfiCourse.Is_deleted = true;
                verfiCourse.Deleted_by = "Admin Approval";
                verfiCourse.Deleted_on = DateTime.UtcNow;
                await _courseRepo.RemoveVerifyCourse(verfiCourse);
                await _courseRepo.SaveChangeAsyncCustom();

                await _publishEndpoint.Publish(new CourseApproved(verfiCourse.Id, false));
            }
        }
    }
}
