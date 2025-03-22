
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.RepoInterfaces.AuthRepo;
using UserService.Application.Auth.UserAuth.Commands.ResetPw;

namespace UserService.Application.Auth.UserAuth.Commands
{
    public class ResetPasswordCommandHandler:IRequestHandler<ResetPasswordCommand,bool>
    {
        private readonly IUserAuthRepo _userAuthRepo;

        public ResetPasswordCommandHandler(IUserAuthRepo userAuthRepo)
        {
            _userAuthRepo = userAuthRepo;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userAuthRepo.GetUserByEmail(request.Email);

                if(user == null)
                {
                    throw new Exception("User Not Found");
                }
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);

                user.Password = hashPassword;
                await _userAuthRepo.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }

}
