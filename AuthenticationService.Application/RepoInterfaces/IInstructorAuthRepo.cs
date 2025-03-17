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
        Task<bool> AddVerifyUser(VerifyUser verifyUser);
        Task<VerifyUser> GetVerifyUserByEmail(string email);
        Task<bool> RemoveVerifyUser(VerifyUser verifyUser);
        Task<bool> SaveChangesAsync();
    }
}
