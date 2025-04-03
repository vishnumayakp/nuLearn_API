using CourseService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Application.RepoInterface.ICategoryRepoInterface
{
    public interface ICategoryRepo
    {
        Task<Category> AddCategory(Category category);
        Task<bool> UpdateCategory(Category category);
        Task<bool> DeleteCategory(Guid Id);
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(Guid id);
        Task<Category> GetCategoryByName(string name);
        Task<bool> ExistsAsync(Guid categoryId);
        Task<bool> SaveChangeAsyncCustom();
    }
}
