using CourseService.Application.DTO.CategoryDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Application.Categories.Query
{
    public class CategoriesViewQuery:IRequest<List<CategoryDto>>
    {
    }
}
