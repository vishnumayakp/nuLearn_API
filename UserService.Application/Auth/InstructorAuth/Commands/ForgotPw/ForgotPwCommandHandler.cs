using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.RepoInterfaces.AuthRepo;
using UserService.Application.ServiceInterfaces.AuthServiceInterface;
using UserService.Domain.Entities;

namespace UserService.Application.Auth.InstructorAuth.Commands.ForgotPw
{
    public class ForgetPwCommandHandler : IRequestHandler<ForgotPwCommand, bool>
    {
        private readonly IInstructorAuthRepo _authRepo;
        private readonly IEmailService _emailService;

        public ForgetPwCommandHandler(IInstructorAuthRepo authRepo, IEmailService emailService)
        {
            _authRepo = authRepo;
            _emailService = emailService;
        }

        public async Task<bool> Handle(ForgotPwCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _authRepo.GetInstructorByEmail(request.Email);
                if (user == null)
                {
                    throw new Exception("User Not Found");
                }
                var verifiedUser = await _authRepo.GetVerifyUserByEmail(request.Email);

                if (verifiedUser != null)
                {
                    _authRepo.RemoveVerifyUser(verifiedUser);
                    await _authRepo.SaveChangesAsync();
                }

                // Generate otp
                Random random = new Random();
                int otp = random.Next(100000, 100000);

                var res = await _emailService.SendOtp(request.Email, otp);

                if (res)
                {
                    var currentTime = TimeOnly.FromDateTime(DateTime.Now);
                    var expiryTime = currentTime.AddMinutes(5);

                    //store data in verify user
                    var userEntity = new VerifyInstructor();
                    userEntity.Name = user.Name;
                    userEntity.Email = user.Email;
                    userEntity.Password = user.Password;
                    userEntity.Otp = otp;
                    userEntity.Certificate_Url = user.Certificate_Url;
                    userEntity.Expire_time = expiryTime;

                    await _authRepo.AddVerifyUser(userEntity);
                    await _authRepo.SaveChangesAsync();

                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
