using Contracts.Events.Response;
using CourseService.Application.RepoInterface.ICategoryRepoInterface;
using CourseService.Domain.Entities;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Application.Consumers
{
    public class AdminVerifiedConsumer: IConsumer<AdminVerified>
    {
        private readonly ICategoryRepo _categoryRepo;

        public AdminVerifiedConsumer(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task Consume(ConsumeContext<AdminVerified>context)
        {
            if (context.Message.IsVerified)
            {
                var category = new Category
                {
                    Category_Name = context.Headers.Get<string>("CategoryName"),
                    Image = context.Message.Image
                };
            await _categoryRepo.AddCategory(category);
                await _categoryRepo.SaveChangeAsyncCustom();
                Console.WriteLine($"✅ Category '{category.Category_Name}' added successfully.");
            }
            else
            {
                Console.WriteLine($"❌ Admin verification failed. Category not added.");
            }
        }
    }
}
