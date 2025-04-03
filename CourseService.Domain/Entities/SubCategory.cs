using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Domain.Entities
{
    public class SubCategory:AuditableEntity
    {
        public Guid Id { get; set; }
        public string SubCategory_Name { get; set; }
    }
}
