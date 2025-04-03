using Contracts.Events.Request;
using Contracts.Events.Response;
using CourseService.Application.Categories.Command;
using CourseService.Application.RepoInterface.ICategoryRepoInterface;
using CourseService.Application.ServiceInterface;
using CourseService.Domain.Entities;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.Application.Categories.CommandHandler
{
    public class CategoryAddCommandHandler : IRequestHandler<CategoryAddCommand, bool>
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IRequestClient<AdminVerificationRequested> _requestClient;

        public CategoryAddCommandHandler(ICategoryRepo categoryRepo, ICloudinaryService cloudinaryService, IPublishEndpoint publishEndpoint, IRequestClient<AdminVerificationRequested> requestClient)
        {
            _categoryRepo = categoryRepo;
            _cloudinaryService = cloudinaryService;
            _publishEndpoint = publishEndpoint;
            _requestClient = requestClient;
        }

        public async Task<bool> Handle(CategoryAddCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine("🚀 Sending AdminVerificationRequested event...");

                var imageUrl = await _cloudinaryService.UploadCategoryImage(request.Image);

             var response =  await _requestClient.GetResponse<AdminVerified>(new AdminVerificationRequested(request.AdminId, request.Name, imageUrl), cancellationToken);
                if (!response.Message.IsVerified)
                {
                    return false;
                }

                var category = new Category
                {
                    Category_Name = request.Name,
                    Image = imageUrl,
                    Created_by = "Initial Create",
                    Created_on = DateTime.UtcNow,
                    Updated_by = "Initial Create",
                    Updated_on = DateTime.UtcNow,
                    Deleted_by = "Initial Create",
                    Deleted_on = DateTime.UtcNow
                };

                await _categoryRepo.AddCategory(category);
                await _categoryRepo.SaveChangeAsyncCustom();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Exception in Handle: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }


    }
}
