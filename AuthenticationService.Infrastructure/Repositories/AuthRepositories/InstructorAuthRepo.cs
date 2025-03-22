using UserService.Application.RepoInterfaces;
using UserService.Domain.Entities;
using UserService.Domain.Entity;
using UserService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.RepoInterfaces.AuthRepo;

namespace UserService.Infrastructure.Repositories.AuthRepositories
{
    public class InstructorAuthRepo : IInstructorAuthRepo
    {
        private readonly AppDbContext _appDbContext;

        public InstructorAuthRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Instructor> GetInstructorByEmail(string email)
        {
            try
            {
                var instructor = await _appDbContext.Instructors.FirstOrDefaultAsync(e => e.Email == email);
                if (instructor == null)
                {
                    Console.WriteLine($"Instructor not found for email: {email}");
                }
                else
                {
                    Console.WriteLine($"Instructor found: {instructor.Email}");
                }
                return instructor;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching instructor: {ex.Message}");
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }



        public async Task<bool> AddInstructor(Instructor instructor)
        {
            await _appDbContext.Instructors.AddAsync(instructor);
            return true;
        }

        public async Task<bool> AddVerifyUser(VerifyInstructor instructor)
        {
            await _appDbContext.VerifyInstructors.AddAsync(instructor);
            return true;
        }

        public async Task<VerifyInstructor> GetVerifyUserByEmail(string email)
        {
            var verifiedInstructor = await _appDbContext.VerifyInstructors.FirstOrDefaultAsync(e => e.Email == email);
            return verifiedInstructor;
        }

        public async Task<bool> RemoveVerifyUser(VerifyInstructor verifyInstructor)
        {
            _appDbContext.VerifyInstructors.Remove(verifyInstructor);
            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
