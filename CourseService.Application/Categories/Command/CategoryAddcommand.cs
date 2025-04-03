using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Application.Categories.Command
{
    public record CategoryAddCommand(Guid AdminId, string Name, IFormFile Image) : IRequest<bool>;

    public record CategoryAddCommandWithoutAdmin(string Name, IFormFile Image) : IRequest<bool>;


}
