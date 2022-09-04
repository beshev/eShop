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
        private readonly IRepository<TemplateCategory> templateCategoriesRepo;
        private readonly ICloudinaryService cloudinaryService;

        public TemplateService(
            IRepository<Template> templateRepo,
            IRepository<TemplateCategory> templateCategoriesRepo,
            ICloudinaryService cloudinaryService)
        {
            this.templateRepo = templateRepo;
            this.templateCategoriesRepo = templateCategoriesRepo;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task AddAsync(string name, string description, decimal price, IFormFile image, int imagesFixedCount, bool isBaseModel, bool hasCustomText, int templateCategoryId, IEnumerable<int> categoriesIds)
        {
            var categories = await (from categoriesTable in this.templateCategoriesRepo.All()
                                        where categoriesIds.Any(id => categoriesTable.Id.Equals(id))
                                    select categoriesTable).ToListAsync();

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
                TemplateCategories = categories,
            };

            await this.templateRepo.AddAsync(template);
            await this.templateRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<TModel>> GetAllAsync<TModel>(int? categoryId = null, int? subCategoryId = null, int skip = 0, int? take = null)
        {
            var templates = this.templateRepo.AllAsNoTracking();

            if (categoryId.HasValue)
            {
                templates = templates.Where(t => t.TemplateCategories.Any(tp => tp.Id.Equals(categoryId)));
            }

            if (subCategoryId.HasValue)
            {
                // TODO: Add subcategory
                // templates = templates.Where(x => x.TemplateCategories.Id.Equals(categoryId));
            }

            if (take.HasValue)
            {
                templates = templates.Skip(skip).Take(take.Value);
            }

            return await templates
                .To<TModel>()
                .ToListAsync();
        }

        public async Task CreateCategoryAsync(string name, IEnumerable<int> tempalteIds)
        {
            var templateCategory = new TemplateCategory
            {
                Name = name,
            };

            if (tempalteIds is not null && tempalteIds.Any())
            {
                templateCategory.Templates = await (from templates in this.templateRepo.All()
                                                        where tempalteIds.Any(id => templates.Id.Equals(id))
                                                    select templates).ToListAsync();
            }

            await this.templateCategoriesRepo.AddAsync(templateCategory);
            await this.templateCategoriesRepo.SaveChangesAsync();
        }

        public async Task RemoveCategoryAsync(int categoryId)
        {
            var category = await this.templateCategoriesRepo
                .All()
                .FirstOrDefaultAsync(x => x.Id.Equals(categoryId));

            this.templateCategoriesRepo.Delete(category);
            await this.templateCategoriesRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<TModel>> GetCategoriesAsync<TModel>()
         => await this.templateCategoriesRepo
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

        public async Task<bool> IsCompatibleWithProductAsync(int templateId, int categoryId)
            => await this.templateCategoriesRepo
            .AllAsNoTracking()
            .AnyAsync(c => c.Id.Equals(categoryId) && c.Templates.Any(t => t.Id.Equals(templateId)));

        public async Task<int> GetCountAsync(int? categoryId = null, int? subCategoryId = null)
        {
            var query = this.templateRepo
            .AllAsNoTracking();

            if (categoryId.HasValue)
            {
                query = query.Where(template => template.TemplateCategories.Any(x => x.Id.Equals(categoryId.Value)));
            }

            if (subCategoryId.HasValue)
            {
                // TODO: Add subcategory
                // uery = query.Where(template => template.TemplateCategoryId.Equals(subCategoryId.Value));
            }

            return await query.CountAsync();
        }

        public async Task<TModel> GetCategoryAsync<TModel>(int categoryId)
            => await this.templateCategoriesRepo
            .AllAsNoTracking()
            .Where(x => x.Id.Equals(categoryId))
            .To<TModel>()
            .FirstOrDefaultAsync();
    }
}
