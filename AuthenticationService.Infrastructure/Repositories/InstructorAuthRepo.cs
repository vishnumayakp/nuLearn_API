using AuthenticationService.Application.RepoInterfaces;
using AuthenticationService.Domain.Entities;
using AuthenticationService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using PlotLink.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Infrastructure.Repositories
{
    public class InstructorAuthRepo:IInstructorAuthRepo
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
                return instructor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<bool> AddInstructor(Instructor instructor)
        {
            await _appDbContext.Instructors.AddAsync(instructor);
            return true;
        }

        public async Task<bool> AddVerifyUser(VerifyUser instructor)
        {
            await _appDbContext.VerifyUsers.AddAsync(instructor);
            return true;
        }

        public async Task<VerifyUser> GetVerifyUserByEmail(string email)
        {
            var verifiedInstructor = await _appDbContext.VerifyUsers.FirstOrDefaultAsync(e => e.Email == email);
            return verifiedInstructor;
        }

        public async Task<bool> RemoveVerifyUser(VerifyUser verifyInstructor)
        {
             _appDbContext.VerifyUsers.Remove(verifyInstructor);
            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
