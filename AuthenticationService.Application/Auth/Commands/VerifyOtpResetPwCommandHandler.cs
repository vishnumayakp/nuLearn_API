﻿using AuthenticationService.Application.RepoInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.Auth.Commands
{
    public class VerifyOtpResetPwCommandHandler:IRequestHandler<VerifyOtpResetPwCommand,bool>
    {
        private readonly IUserAuthRepo _userAuthRepo;

        public VerifyOtpResetPwCommandHandler(IUserAuthRepo userAuthRepo)
        {
            _userAuthRepo = userAuthRepo;
        }

        public async Task<bool> Handle(VerifyOtpResetPwCommand request , CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userAuthRepo.GetVerifyUserByEmail(request.Email);
                if (user == null)
                {
                    throw new Exception("User Not Found");
                }

                TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);

                if(currentTime<= user.Expire_time)
                {
                    await _userAuthRepo.RemoveVerifyUser(user);
                    await _userAuthRepo.SaveChangesAsync();
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
