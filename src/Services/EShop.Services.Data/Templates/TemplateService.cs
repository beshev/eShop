namespace EShop.Services.Data.Templates
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EShop.Data.Common.Repositories;
    using EShop.Data.Models;
    using Eshop.Services.Cloudinary;
    using EShop.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class TemplateService : ITemplateService
    {
        private readonly IRepository<Template> templateRepo;
        private readonly ICloudinaryService cloudinaryService;

        public TemplateService(
            IRepository<Template> templateRepo,
            ICloudinaryService cloudinaryService)
        {
            this.templateRepo = templateRepo;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task AddAsync(string name, string description, decimal price, IFormFile image, int imagesFixedCount, bool isBaseModel, bool hasCustomText, int templateCategoryId, IEnumerable<int> productsIds)
        {
            var cloudFolder = $"templates/{name}";

            var template = new Template
            {
                Name = name,
                Description = description,
                Price = price,
                ImagesFixedCount = imagesFixedCount,
                TemplateCategoryId = templateCategoryId,
                HasCustomText = hasCustomText,
                IsBaseModel = isBaseModel,
                ImageUrl = await this.cloudinaryService.UploadAsync(image, cloudFolder),
                TemplateProducts = productsIds.Select(productId => new ProductTemplate
                {
                    ProdcutId = productId,
                }).ToList(),
            };

            await this.templateRepo.AddAsync(template);
            await this.templateRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<TModel>> GetAllAsync<TModel>(string category = null, int skip = 0, int? take = null)
        {
            var templates = this.templateRepo.AllAsNoTracking();

            if (string.IsNullOrWhiteSpace(category) == false)
            {
                templates = templates.Where(x => x.TemplateCategory.Name.Equals(category));
            }

            if (take.HasValue)
            {
                templates = templates.Skip(skip).Take(take.Value);
            }

            return await templates
                .To<TModel>()
                .ToListAsync();
        }
    }
}
