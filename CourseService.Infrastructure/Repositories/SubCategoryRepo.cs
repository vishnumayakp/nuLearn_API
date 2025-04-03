using CourseService.Application.RepoInterface.SubCateRepoInterface;
using CourseService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Infrastructure.Repositories
{
    public class SubCategoryRepo:ISubCategoryRepo
    {
        private readonly AppDbContext _appDbContext;

        public SubCategoryRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<SubCategory> AddSubCatogory(SubCategory subCategory)
        {
            await _appDbContext.AddAsync(subCategory);
            return subCategory;
        }

        public async Task<bool> UpdateSubCatogory(SubCategory subCategory)
        {
             _appDbContext.SubCategories.Update(subCategory);
            return true;
        }

        public async Task<bool> DeleteSubCatogory(SubCategory subCategory)
        {
            var sub = await _appDbContext.Categories.FirstOrDefaultAsync(s=>s.Id == subCategory.Id);
            sub.Is_deleted= true;
            return true;
        }

        public async Task<List<SubCategory>> GetAllSubCatogories()
        {
            var sub = await _appDbContext.SubCategories.ToListAsync();
            return sub;
        }

        public async Task<SubCategory> GetSubCatogoryById(Guid id)
        {
            var sub = await _appDbContext.SubCategories.FirstOrDefaultAsync(s=>s.Id == id);
            return sub;
        }

        public async Task<SubCategory> GetSubCatogoryByName(string name)
        {
            var sub = await _appDbContext.SubCategories.FirstOrDefaultAsync(s=>s.SubCategory_Name == name);
            return sub;
        }

        public async Task<bool> SaveChangeAsyncCustom()
        {
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid categoryId)
        {
            return await _appDbContext.SubCategories.AnyAsync(a => a.Id == categoryId);
        }
    }
}
