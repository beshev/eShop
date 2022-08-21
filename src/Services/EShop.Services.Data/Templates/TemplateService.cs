namespace EShop.Services.Data.Templates
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EShop.Common;
    using EShop.Data.Common.Repositories;
    using EShop.Data.Models;
    using Eshop.Services.Cloudinary;
    using EShop.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class TemplateService : ITemplateService
    {
        private readonly IRepository<Template> templateRepo;
        private readonly IRepository<TemplateCategory> templateCategoryRepo;
        private readonly IRepository<ProductTemplate> productTemplateRepo;
        private readonly ICloudinaryService cloudinaryService;

        public TemplateService(
            IRepository<Template> templateRepo,
            IRepository<TemplateCategory> templateCategoryRepo,
            IRepository<ProductTemplate> productTemplateRepo,
            ICloudinaryService cloudinaryService)
        {
            this.templateRepo = templateRepo;
            this.templateCategoryRepo = templateCategoryRepo;
            this.productTemplateRepo = productTemplateRepo;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task AddAsync(string name, string description, decimal price, IFormFile image, int imagesFixedCount, bool isBaseModel, bool hasCustomText, int templateCategoryId, IEnumerable<int> productsIds)
        {
            var template = new Template
            {
                Name = name,
                Description = description,
                Price = price,
                ImagesFixedCount = imagesFixedCount,
                TemplateCategoryId = templateCategoryId,
                HasCustomText = hasCustomText,
                IsBaseModel = isBaseModel,
                ImageUrl = await this.cloudinaryService.UploadAsync(image, string.Format(GlobalConstants.TemplateCloundFolderName, name)),
                TemplateProducts = productsIds.Select(productId => new ProductTemplate
                {
                    ProductId = productId,
                }).ToList(),
            };

            await this.templateRepo.AddAsync(template);
            await this.templateRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<TModel>> GetAllAsync<TModel>(int? productId = null, int? categoryId = null, int skip = 0, int? take = null)
        {
            var templates = this.templateRepo.AllAsNoTracking();

            if (productId.HasValue)
            {
                templates = templates.Where(t => t.TemplateProducts.Any(tp => tp.ProductId.Equals(productId)));
            }

            if (categoryId.HasValue)
            {
                templates = templates.Where(x => x.TemplateCategory.Id.Equals(categoryId));
            }

            if (take.HasValue)
            {
                templates = templates.Skip(skip).Take(take.Value);
            }

            return await templates
                .To<TModel>()
                .ToListAsync();
        }

        public async Task CreateCategoryAsync(string name)
        {
            await this.templateCategoryRepo.AddAsync(new TemplateCategory { Name = name });
            await this.templateCategoryRepo.SaveChangesAsync();
        }

        public async Task RemoveCategoryAsync(int categoryId)
        {
            var category = await this.templateCategoryRepo
                .All()
                .FirstOrDefaultAsync(x => x.Id.Equals(categoryId));

            this.templateCategoryRepo.Delete(category);
            await this.templateCategoryRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<TModel>> GetCategoriesAsync<TModel>()
         => await this.templateCategoryRepo
            .AllAsNoTracking()
            .To<TModel>()
            .ToListAsync();

        public async Task<TModel> GetByIdAsync<TModel>(int id)
            => await this.templateRepo
            .AllAsNoTracking()
            .Where(x => x.Id.Equals(id))
            .To<TModel>()
            .FirstOrDefaultAsync();

        public async Task DeleteByIdAsync(int id)
        {
            var template = await this.templateRepo
                   .All()
                   .FirstOrDefaultAsync(template => template.Id.Equals(id));

            await this.cloudinaryService.DeleteAsync(string.Format(GlobalConstants.TemplateCloundFolderName, template.Name));
            this.templateRepo.Delete(template);
            await this.templateRepo.SaveChangesAsync();
        }

        public async Task<bool> IsCompatibleWithProductAsync(int templateId, int productId)
            => await this.productTemplateRepo
            .AllAsNoTracking()
            .AnyAsync(x => x.ProductId.Equals(productId) && x.TemplateId.Equals(templateId));
    }
}
