using CourseService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Application.RepoInterface.CourseRepoInterface
{
    public interface ICourseRepo
    {
        Task<Course> AddCourse(Course course);
        Task<Course> UpdateCourse(Course course);
        Task<bool> DeleteCourse(Guid id);
        Task<List<Course>> GetAll();
        Task<Course> GetById(Guid id);
        Task<bool> SaveChangeAsyncCustom();
        Task<bool> AddVerifyCourse(VerifyCourse course);
        Task<List<VerifyCourse>> GetAllVerifyCourse();
        Task<VerifyCourse> GetVerifyCourseById(Guid id);
        Task<bool> RemoveVerifyCourse(VerifyCourse verifyCourse);
        Task<List<Course>> GetCourseByInstructorId(Guid instructorId);

    }
}
