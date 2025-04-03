using CourseService.Application.RepoInterface.CourseRepoInterface;
using CourseService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Infrastructure.Repositories
{
    public class VideoRepo:IVideoRepo
    {
        private readonly AppDbContext _appDbContext;
       

        public VideoRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddVideo(List<Video> videos)
        {
            await _appDbContext.AddRangeAsync(videos);
            await _appDbContext.SaveChangesAsync();
        }

    }
}
