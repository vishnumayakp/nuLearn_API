using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Application.Common
{
    public class AppSettings
    {
        public string DbConnectionString { get; set; }
        public string JwtSecretKey { get; set; }
        public string CloudinaryCloudName { get; set; }
        public string CloudinaryApiKey { get; set; }
        public string CloudinaryApiSecrut { get; set; }

        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
