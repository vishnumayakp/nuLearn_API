using AuthenticationService.Application.RepoInterfaces;
using AuthenticationService.Application.ServiceInterfaces;
using MediatR;
using PlotLink.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.InstructorAuth.Commands
{
    public class ForgetPwCommandHandler:IRequestHandler<ForgetPwCommand,bool>
    {
        private readonly IInstructorAuthRepo _authRepo;
        private readonly IEmailService _emailService;

        public ForgetPwCommandHandler(IInstructorAuthRepo authRepo, IEmailService emailService)
        {
            _authRepo = authRepo;
            _emailService = emailService;
        }

        public async Task<bool> Handle(ForgetPwCommand request, CancellationToken cancellationToken)
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
                    var userEntity = new VerifyUser();
                    userEntity.Name = user.Name;
                    userEntity.Email = user.Email;
                    userEntity.Password = user.Password;
                    userEntity.Otp = otp;
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
