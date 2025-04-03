using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entities;
using UserService.Domain.Entity;

namespace UserService.Application.RepoInterfaces.ViewRepo
{
    public interface IInstructorViewRepo
    {
        Task<List<Instructor>> GetAllInstructor();
        Task<Instructor> GetInstructorById(Guid id);
        Task<Instructor> GetInstructorByName(string name);
        Task<bool> SaveChangeAsyncCustom();
        Task<Instructor> UpdateUserAsyn(Instructor instructor);
        Task<bool> InstructorExists(Guid instructorId);
    }
}
