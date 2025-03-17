using AuthenticationService.Domain.Entities;
using AuthenticationService.Domain.Entity;
using PlotLink.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.RepoInterfaces
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
