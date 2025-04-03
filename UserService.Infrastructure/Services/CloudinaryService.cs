using UserService.Application.ServiceInterfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using UserService.Application.Common;

namespace UserService.Infrastructure.Services
{
    public class CloudinaryService:ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration configuration, Appsettings appSettings)
        {

            var cloudName = appSettings.CloudinaryCloudName;
            var apiKey = appSettings.CloudinaryApiKey;
            var apiSecret = appSettings.CloudinaryApiSecrut;

            if (string.IsNullOrEmpty(cloudName) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
            {
                throw new Exception("Cloudinary configuration is missing or incomplete");
            }

            var account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadCertificate(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file provided.");
            }
            var allowedFileTypes = new[] { ".pdf", ".png", ".jpg", ".jpeg" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedFileTypes.Contains(fileExtension))
            {
                throw new ArgumentException("Invalid file type. Only PDF, PNG, JPG, and JPEG are allowed.");
            }

            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new RawUploadParams
                {
                    File = new FileDescription(file.FileName, stream), 
                    Folder = "certificates" 
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null)
                {
                    throw new Exception($"Cloudinary upload failed: {uploadResult.Error.Message}");
                }
                return uploadResult.SecureUrl?.ToString() ?? throw new Exception("Cloudinary response did not contain a SecureUrl.");
            }
        }

        public async Task<string> UploadProfileImage(IFormFile file)
        {

            Console.WriteLine($"Cloud Name: {Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME")}");
            Console.WriteLine($"API Key: {Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY")}");
            Console.WriteLine($"API Secret: {Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET")}");

            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file provided.");
            }
            Console.WriteLine($"Uploading: {file.FileName}");


            using (var stream = file.OpenReadStream())
            {
                var upLoadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Width(500).Height(500).Crop("fill"),
                };
                var upLoadResult = await _cloudinary.UploadAsync(upLoadParams);



                if (upLoadResult.Error != null)
                {
                    throw new Exception($"Cloudinary upload failed: {upLoadResult.Error.Message}");
                }

                if (string.IsNullOrEmpty(upLoadResult.SecureUrl?.ToString()))
                {
                    throw new Exception("Cloudinary response did not contain a SecureUrl.");
                }

                return upLoadResult.SecureUrl.ToString();


            }
        }

    }
}
