using CourseService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Application.RepoInterface.SubCateRepoInterface
{
    public interface ISubCategoryRepo
    {
        Task<SubCategory> AddSubCatogory(SubCategory subCategory);
        Task<bool> UpdateSubCatogory(SubCategory subCategory);
        Task<bool> DeleteSubCatogory(SubCategory subCategory);
        Task<List<SubCategory>> GetAllSubCatogories();
        Task<SubCategory> GetSubCatogoryById(Guid id);
        Task<SubCategory> GetSubCatogoryByName(string name);
        Task<bool> ExistsAsync(Guid subCategoryId);
        Task<bool> SaveChangeAsyncCustom();
    }
}
