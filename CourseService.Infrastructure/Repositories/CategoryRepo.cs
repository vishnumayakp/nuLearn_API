using CourseService.Application.RepoInterface.ICategoryRepoInterface;
using CourseService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Infrastructure.Repositories
{
    public class CategoryRepo:ICategoryRepo
    {
        private readonly AppDbContext _appDbContext;

        public CategoryRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Category> AddCategory(Category category)
        {
            await _appDbContext.Categories.AddAsync(category);
            return category;
        }

        public async Task<bool> UpdateCategory(Category category)
        {
             _appDbContext.Categories.Update(category);
            return true;
        }

        

        public async Task<List<Category>> GetAllCategories()
        {
            var categories  =  await _appDbContext.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategoryById(Guid id)
        {
            var category = await _appDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            return category;
        }
        public async Task<bool> DeleteCategory(Guid Id)
        {
            var category = await _appDbContext.Categories.FirstOrDefaultAsync(c => c.Id == Id);
            category.Is_deleted = true;
            return true;
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            var category = _appDbContext.Categories.FirstOrDefault(c => c.Category_Name == name);
            return category;
        }

        public async Task<bool> SaveChangeAsyncCustom()
        {
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid categoryId)
        {
            return await _appDbContext.Categories.AnyAsync(a => a.Id == categoryId);
        }
    }
}
