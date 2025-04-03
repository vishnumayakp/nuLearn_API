using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Application.ServiceInterface
{
    public interface ICloudinaryService
    {
        Task<string> UploadCategoryImage( IFormFile formFile);
        Task<string> UploadCourseImage(IFormFile file);
        Task<List<string>> UploadVideoAsync(IEnumerable<IFormFile> files);
        Task<List<string>> UploadDocumentsAsync(IEnumerable<IFormFile> files);
    }
}
