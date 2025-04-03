using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.RepoInterfaces.AuthRepo;

namespace UserService.Application.Auth.InstructorAuth.Commands.ResetPw
{
    public class ResetPwCommandHandler : IRequestHandler<ResetPwCommand, bool>
    {
        private readonly IInstructorAuthRepo _authRepo;

        public ResetPwCommandHandler(IInstructorAuthRepo authRepo)
        {
            _authRepo = authRepo;
        }

        public async Task<bool> Handle(ResetPwCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _authRepo.GetInstructorByEmail(request.Email);

                if (user == null)
                {
                    throw new Exception("User Not Found");
                }
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);

                user.Password = hashPassword;
                await _authRepo.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
