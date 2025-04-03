using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using MimeKit.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.RepoInterfaces.ViewRepo;
using UserService.Application.ServiceInterfaces;
using UserService.Application.ServiceInterfaces.UserViewServiceInterface;

namespace UserService.Infrastructure.Services.UserViewService
{
    public class UserViewService:IUserViewService
    {
        private readonly IUserViewRepo _userViewRepo;
        private readonly ICloudinaryService _cloudinaryService;
        public UserViewService(IUserViewRepo userViewRepo,ICloudinaryService cloudinaryService)
        {
                _userViewRepo = userViewRepo;
                _cloudinaryService = cloudinaryService;
        }

        public async Task<bool> UpdateUserProfile(Guid userId, string name, IFormFile profile)
        {
            try
            {
                var user = await _userViewRepo.GetUserById(userId);
                if (user == null)
                {
                    throw new Exception("User Not Found ");
                }

                if(name != null)
                {
                    user.Name = name;
                }

                if (profile == null && profile != null)
                {
                    string profileUrl = await _cloudinaryService.UploadProfileImage(profile);
                    user.Profile = profile.ToString();
                }

                await _userViewRepo.UpdateUserAsyn(user);
                return true;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
