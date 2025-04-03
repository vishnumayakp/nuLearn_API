using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CourseService.Application.Common;
using CourseService.Application.ServiceInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Infrastructure.Services
{
    public class CloudinaryService:ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration configuration, AppSettings appSettings)
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

        public async Task<string> UploadCategoryImage(IFormFile file)
        {


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

        public async Task<string> UploadCourseImage(IFormFile file)
        {


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

        public async Task<List<string>> UploadVideoAsync(IEnumerable<IFormFile> files)
        {
            var uploadTasks = files.Select(async file =>
            {
                if (file == null || file.Length == 0)
                    throw new Exception("No file uploaded");

                if (file.Length > 500 * 1024 * 1024)
                    throw new Exception($"File too large: {file.FileName}");

                await using var stream = file.OpenReadStream();

                var uploadParams = new VideoUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = "course_videos",
                    Overwrite = true,
                    EagerAsync = true,
                    EagerTransforms = new List<Transformation>
            {
                new Transformation().Quality("auto").FetchFormat("mp4")
            }
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null || string.IsNullOrEmpty(uploadResult.SecureUrl?.ToString()))
                {
                    throw new Exception($"Upload failed for {file.FileName}: {uploadResult.Error?.Message}");
                }

                return uploadResult.SecureUrl.ToString();
            });

            return (await Task.WhenAll(uploadTasks)).ToList();
        }


        public async Task<List<string>> UploadDocumentsAsync(IEnumerable<IFormFile> files)
        {
            var uploadTasks = files.Select(async file =>
            {
                if (file == null || file.Length == 0)
                    throw new Exception("No file uploaded");

                await using var stream = file.OpenReadStream();

                var uploadParams = new RawUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = "course_documents",
                    Overwrite = true,
                    Context = new StringDictionary
            {
                { "original_filename", file.FileName }
            }
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null)
                {
                    throw new Exception($"Upload failed for {file.FileName}: {uploadResult.Error.Message}");
                }

                if (string.IsNullOrEmpty(uploadResult.SecureUrl?.ToString()))
                {
                    throw new Exception($"Upload response did not contain a SecureUrl for {file.FileName}.");
                }

                return uploadResult.SecureUrl.ToString();
            });

            return (await Task.WhenAll(uploadTasks)).ToList();
        }

    }
}
