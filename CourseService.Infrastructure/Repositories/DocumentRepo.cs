using CourseService.Application.RepoInterface.CourseRepoInterface;
using CourseService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Infrastructure.Repositories
{
    public class DocumentRepo:IDocumentRepo
    {
        private readonly AppDbContext _appDbContext;

        public DocumentRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddDocument(List<Document> documents)
        {
            await _appDbContext.AddRangeAsync(documents);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
