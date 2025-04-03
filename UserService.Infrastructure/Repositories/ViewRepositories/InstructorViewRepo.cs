using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.RepoInterfaces.ViewRepo;
using UserService.Domain.Entities;
using UserService.Domain.Entity;

namespace UserService.Infrastructure.Repositories.ViewRepositories
{
    public class InstructorViewRepo:IInstructorViewRepo
    {
        private readonly AppDbContext _appDbContext;

        public InstructorViewRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Instructor>> GetAllInstructor()
        {
            try
            {
                var instructors = await _appDbContext.Instructors.ToListAsync();
                return instructors;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Instructor> GetInstructorById(Guid id)
        {
            try
            {
                var instructor = await _appDbContext.Instructors.FirstOrDefaultAsync(i => i.Instructor_Id == id);
                return instructor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Instructor> GetInstructorByName(string name)
        {
            try
            {
                var instructor = await _appDbContext.Instructors.FirstOrDefaultAsync(i => i.Name == name);
                return instructor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> SaveChangeAsyncCustom()
        {
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Instructor> UpdateUserAsyn(Instructor instructor)
        {
            try
            {
                _appDbContext.Instructors.Update(instructor);
                return await _appDbContext.Instructors.FindAsync(instructor.Instructor_Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InstructorExists(Guid instructorId)
        {
            await _appDbContext.Instructors.AnyAsync(i => i.Instructor_Id == instructorId);
            return true;
        }
    }

}
