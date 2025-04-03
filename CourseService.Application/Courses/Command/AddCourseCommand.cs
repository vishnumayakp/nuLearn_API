using CourseService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Application.Courses.Command
{
    public record AddCourseCommand(
    Guid InstructorId,
    Guid CategoryId,
    string CourseName,
    string Description,
    CourseType Type,
    IFormFile ImageUrl,
    decimal MRP,
    decimal Price,
    List<IFormFile> Videos,
    List<IFormFile> Documents
) : IRequest<Guid>;

    public record AddCourseRequest(
    Guid CategoryId,
    string CourseName,
    string Description,
    CourseType Type,
    IFormFile ImageUrl,
    decimal MRP,
    decimal Price,
    List<IFormFile> Videos,
    List<IFormFile> Documents
        ):IRequest<Guid>;
}
