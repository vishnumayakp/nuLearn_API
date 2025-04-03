using CourseService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Application.RepoInterface.CourseRepoInterface
{
    public interface IVideoRepo
    {
        Task AddVideo(List<Video> videos);
    }
}
