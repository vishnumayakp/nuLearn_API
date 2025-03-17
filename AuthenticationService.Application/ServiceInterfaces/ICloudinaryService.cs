using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.ServiceInterfaces
{
    public interface ICloudinaryService
    {
        Task<string> UploadCertificate(IFormFile file);
    }
}
