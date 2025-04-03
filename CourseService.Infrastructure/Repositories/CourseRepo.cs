using CourseService.Application.RepoInterface.CourseRepoInterface;
using CourseService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Infrastructure.Repositories
{
    public class CourseRepo:ICourseRepo
    {
        private readonly AppDbContext _appDbContext;

        public CourseRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Course> AddCourse(Course course)
        {
            await _appDbContext.Courses.AddAsync(course);
            return course;
        }

        public async Task<Course> UpdateCourse(Course course)
        {
             _appDbContext.Courses.Update(course);
            return course;
        }

        public async Task<bool> DeleteCourse(Guid id)
        {
            var course = await _appDbContext.Courses.FirstOrDefaultAsync(c => c.Course_Id == id);
            course.Is_deleted= true;
            return true;    
        }

        public async Task<List<Course>> GetAll()
        {
            var courses = await _appDbContext.Courses.ToListAsync();
            return courses;
        }

        public async Task<Course> GetById(Guid id)
        {
            var course = await _appDbContext.Courses
                .Include(v=>v.VideoUrls)
                .Include(d=>d.DocumentUrls)
                .FirstOrDefaultAsync(c => c.Course_Id == id);
            return course;
        }

        public async Task<bool> SaveChangeAsyncCustom()
        {
            await _appDbContext.SaveChangesAsync();
            return true;
        }

       public async Task<bool> AddVerifyCourse(VerifyCourse course)
        {
            await _appDbContext.VerifyCourses.AddAsync(course);
            return true;
        }

        public async Task<List<VerifyCourse>> GetAllVerifyCourse()
        {
            var verifiedCourse = await _appDbContext.VerifyCourses.Where(c=>!c.Is_deleted).ToListAsync();
            return verifiedCourse;
        }

        public async Task<bool> RemoveVerifyCourse(VerifyCourse verifyCourse)
        {
             _appDbContext.VerifyCourses.Remove(verifyCourse);
            return true;
        }

        public async Task<List<Course>> GetCourseByInstructorId(Guid instructorId)
        {
            var courses = await _appDbContext.Courses.Where(i=>i.Instructor_Id== instructorId).ToListAsync();
            return courses;
        }

        public async Task<VerifyCourse> GetVerifyCourseById(Guid id)
        {
            var verifiedCourse = await _appDbContext.VerifyCourses.FirstOrDefaultAsync(c => c.Id == id && !c.Is_deleted);
            return verifiedCourse;
        }
    }
}   
