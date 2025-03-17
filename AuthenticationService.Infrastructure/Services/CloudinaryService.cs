using AuthenticationService.Application.ServiceInterfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Infrastructure.Services
{
    public class CloudinaryService:ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService()
        {
            var cloudname = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME");
            var appkey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY");
            var apisecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET");   
            
            if(string.IsNullOrEmpty(cloudname) || string.IsNullOrEmpty(appkey) || string.IsNullOrEmpty(apisecret))
            {
                throw new Exception("Cloudinary Configuration is MIssing");
            }

            var account = new Account(cloudname, appkey,apisecret);
            _cloudinary= new Cloudinary(account);
        }

        public async Task<string> UploadCertificate(IFormFile file)
        {
            if (file == null || file.Length == 0) return null;

            var allowedFileTypes = new[] { ".pdf", ".png", ".jpg", ".jpeg" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedFileTypes.Contains(fileExtension)) return null;

            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Crop("limit").Width(1000), 
                    Folder = "certificates" 
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult.SecureUrl.ToString();
            }
        }

    }
}
