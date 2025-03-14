using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.Common
{
    public class Appsettings
    {
        public string DbConnectionString { get; set; }
        public string JwtSecretKey { get; set; }
    }
}
