using CourseService.Application.Categories.Query;
using CourseService.Application.DTO.CategoryDTO;
using CourseService.Application.RepoInterface.ICategoryRepoInterface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Application.Categories.QueryHandler
{
    public class CategoriesViewQueryHandler : IRequestHandler<CategoriesViewQuery, List<CategoryDto>>
    {
        private readonly ICategoryRepo _categoryRepo;

        public CategoriesViewQueryHandler(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<List<CategoryDto>> Handle(CategoriesViewQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var categories = await _categoryRepo.GetAllCategories();
                if (categories == null)
                {
                    throw new Exception("Categories Not Found");
                }

                var res = categories.Where(c => c.Is_deleted == false).Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Category = c.Category_Name,
                    Image = c.Image,
                }).ToList();
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
