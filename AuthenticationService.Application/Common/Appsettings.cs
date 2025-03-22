﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Common
{
    public class Appsettings
    {
        public string DbConnectionString { get; set; }
        public string JwtSecretKey { get; set; }
        public string CloudinaryCloudName { get; set; }
        public string CloudinaryApiKey { get; set; }
        public string CloudinaryApiSecrut { get; set; }

    }
}
