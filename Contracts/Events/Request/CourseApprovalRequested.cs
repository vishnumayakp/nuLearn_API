using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Events.Request
{
    public class CourseApprovalRequested
    {
        public Guid CourseId { get; set; }
        public Guid InstructorId { get; set; }  
        public string CourseName { get; set; }
        public string ImageUrl { get; set; }  
        public decimal Price { get; set; }      
        public string Category { get; set; } 
        public List<string> VideoUrls { get; set; } 
        public List<string> DocumentUrls { get; set; }


        public CourseApprovalRequested(Guid courseId, Guid instructorId, string courseName, string imageUrl,
            decimal price, string category, List<string> videoUrls, List<string> documentUrls)
        {
            CourseId = courseId;
            InstructorId = instructorId;
            CourseName = courseName;
            ImageUrl = imageUrl;
            Price = price;
            Category = category;
            VideoUrls = videoUrls;
            DocumentUrls = documentUrls;
        }
    }
}
