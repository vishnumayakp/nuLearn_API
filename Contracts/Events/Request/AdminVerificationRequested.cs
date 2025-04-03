using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Events.Request
{
    public class AdminVerificationRequested
    {
        public  Guid  AdminId { get; set; }
        public string CategoryName { get; set; }
        public string Image { get; set; }

        public AdminVerificationRequested(Guid adminId, string categoryname, string image)
        {
            AdminId = adminId;
            CategoryName=categoryname;
            Image = image;
        }
    } 
}
