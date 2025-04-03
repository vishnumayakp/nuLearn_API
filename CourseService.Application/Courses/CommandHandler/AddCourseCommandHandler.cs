using Contracts.DTO;
using Contracts.Events.Request;
using Contracts.Events.Response;
using CourseService.Application.Courses.Command;
using CourseService.Application.RepoInterface.CourseRepoInterface;
using CourseService.Application.RepoInterface.ICategoryRepoInterface;
using CourseService.Application.ServiceInterface;
using CourseService.Domain.Entities;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Application.Courses.CommandHandler
{
    public class AddCourseCommandHandlder : IRequestHandler<AddCourseCommand, Guid>
    {
        private readonly ICourseRepo _courseRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IRequestClient<InstructorVerificationRequested> _requestClient;

        public AddCourseCommandHandlder(ICourseRepo courseRepo, ICategoryRepo categoryRepo, ICloudinaryService cloudinaryService, IPublishEndpoint publishEndpoint, IRequestClient<InstructorVerificationRequested> requestClient)
        {
            _courseRepo = courseRepo;
            _categoryRepo = categoryRepo;
            _cloudinaryService = cloudinaryService;
            _publishEndpoint = publishEndpoint;
            _requestClient = requestClient;
        }

        public async Task<Guid> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _categoryRepo.GetCategoryById(request.CategoryId);
                if (category == null)
                {
                    throw new Exception("Category not found");
                }


                var verificationEvent = new InstructorVerificationRequested(request.InstructorId);
                var response = await _requestClient.GetResponse<InstructorVerfied>(verificationEvent);
                if (!response.Message.IsVerified)
                {
                    throw new Exception("Instructor Not Authorised");
                }

                var existingCourse = await _courseRepo.GetCourseByInstructorId(request.InstructorId);
                bool isFirstCourse = !existingCourse.Any();

                var image = await _cloudinaryService.UploadCourseImage(request.ImageUrl);

                var videoUrls = request.Videos != null && request.Videos.Any()
                    ? await _cloudinaryService.UploadVideoAsync(request.Videos.Where(v => v.ContentType.StartsWith("video/")).ToList())
                    : new List<string>();


                var documentUrls = request.Documents != null && request.Documents.Any()
                    ? await _cloudinaryService.UploadDocumentsAsync(request.Documents.Where(d => d.ContentType.StartsWith("application/pdf") || d.ContentType.StartsWith("image/")).ToList()) 
                    : new List<string>();




                if (isFirstCourse)
                {
                    var verifyCourse = new VerifyCourse
                    {
                        Id = Guid.NewGuid(),
                        InstructorId = request.InstructorId,
                        CategoryId = request.CategoryId,
                        CourseName = request.CourseName,
                        Description = request.Description,
                        Type = request.Type,
                        ImageUrl = image,
                        MRP = request.MRP,
                        Price = request.Price,
                        VideoUrls = videoUrls,
                        DocumentUrls = documentUrls,
                        Created_by = "Initial Create",
                        Created_on = DateTime.UtcNow,
                        Updated_by = "Initial Create",
                        Updated_on = DateTime.UtcNow,
                        Deleted_by = "Initial Create",
                        Deleted_on = DateTime.UtcNow
                    };
                    await _courseRepo.AddVerifyCourse(verifyCourse);
                    await _courseRepo.SaveChangeAsyncCustom();

                    await _publishEndpoint.Publish(new AdminApprovalRequestDto
                    {
                        InstructorId = verifyCourse.InstructorId,
                        CourseId = verifyCourse.Id,
                        CourseName = verifyCourse.CourseName,
                        CourseImage = verifyCourse.ImageUrl,
                        Price = verifyCourse.Price,
                        Videos = verifyCourse.VideoUrls,
                        Documents = verifyCourse.DocumentUrls,
                        Category = category.Category_Name,
                    });

                    await _publishEndpoint.Publish(new CourseApprovalRequested(verifyCourse.Id, 
                        verifyCourse.InstructorId,
                        verifyCourse.CourseName,
                        verifyCourse.ImageUrl,
                        verifyCourse.Price,
                        category.Category_Name,
                        verifyCourse.VideoUrls,
                        verifyCourse.DocumentUrls
                        ));

                    return verifyCourse.Id;
                }
                
                var courseId = Guid.NewGuid();

                var course = new Course
                {
                    Course_Id = courseId,
                    Instructor_Id = request.InstructorId,
                    Category_Id = request.CategoryId,
                    CourseName = request.CourseName,
                    Description = request.Description,
                    Type = request.Type,
                    ImageUrl = image,
                    MRP = request.MRP,
                    Price = request.Price,
                    Created_by = "Initial Create",
                    Created_on = DateTime.UtcNow,
                    Updated_by = "Initial Create",
                    Updated_on = DateTime.UtcNow,
                    Deleted_by = "Initial Create",
                    Deleted_on = DateTime.UtcNow,

                    VideoUrls = videoUrls.Select(v=> new Video
                    {
                        VideoId=Guid.NewGuid(),
                        CourseId = courseId,
                        VideoUrl = v,
                    }).ToList(),

                    DocumentUrls = documentUrls.Select(d=> new Document
                    {
                        DocumentId=Guid.NewGuid(),
                        CourseId = courseId,
                        DocumentUrl=d,
                    }).ToList(),
                };

                await _courseRepo.AddCourse(course);
                await _courseRepo.SaveChangeAsyncCustom();

               

                return course.Course_Id;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the course.", ex);
            }
        }
    }
}
