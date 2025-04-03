using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.RepoInterfaces.AuthRepo;

namespace UserService.Application.Auth.InstructorAuth.Commands.VerifyOtp
{
    public class VerifyOtpResetPasswordCommandHandler : IRequestHandler<VerifyOtpResetPasswordCommand, bool>
    {
        private readonly IInstructorAuthRepo _authRepo;

        public VerifyOtpResetPasswordCommandHandler(IInstructorAuthRepo authRepo)
        {
            _authRepo = authRepo;
        }

        public async Task<bool> Handle(VerifyOtpResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _authRepo.GetVerifyUserByEmail(request.Email);
                if (user == null)
                {
                    throw new Exception("User Not Found");
                }

                TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);

                if (currentTime <= user.Expire_time)
                {
                    await _authRepo.RemoveVerifyUser(user);
                    await _authRepo.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
