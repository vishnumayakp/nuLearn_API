using UserService.Domain.Entities;
using UserService.Domain.Entity;
using UserService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entities;

namespace UserService.Application.RepoInterfaces.AuthRepo
{
    public interface IInstructorAuthRepo
    {
        Task<Instructor> GetInstructorByEmail(string email);
        Task<bool> AddInstructor(Instructor instructor);
        Task<bool> AddVerifyUser(VerifyInstructor verifyUser);
        Task<VerifyInstructor> GetVerifyUserByEmail(string email);
        Task<bool> RemoveVerifyUser(VerifyInstructor verifyUser);
        Task<bool> SaveChangesAsync();
    }
}
