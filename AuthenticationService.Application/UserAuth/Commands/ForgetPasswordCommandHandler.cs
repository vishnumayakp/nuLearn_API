using AuthenticationService.Application.RepoInterfaces;
using AuthenticationService.Application.ServiceInterfaces;
using MediatR;
using PlotLink.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.Auth.Commands
{
    public class ForgetPasswordCommandHandler:IRequestHandler<ForgetPasswordCommand,bool>
    {
        private readonly IUserAuthRepo _userAuthRepo;
        private readonly IEmailService _emailService;

        public ForgetPasswordCommandHandler(IUserAuthRepo userAuthRepo, IEmailService emailService)
        {
            _userAuthRepo = userAuthRepo;
            _emailService = emailService;
        }

        public async Task<bool> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userAuthRepo.GetUserByEmail(request.Email);
                if(user == null)
                {
                    throw new Exception("User Not Found");
                }
                var verifiedUser = await _userAuthRepo.GetVerifyUserByEmail(request.Email);

                if (verifiedUser != null)
                {
                    _userAuthRepo.RemoveVerifyUser(verifiedUser);
                    await _userAuthRepo.SaveChangesAsync();
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

                    await _userAuthRepo.AddVerifyUser(userEntity);
                    await _userAuthRepo.SaveChangesAsync();
                    
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
