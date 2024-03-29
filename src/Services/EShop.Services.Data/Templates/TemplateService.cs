﻿namespace EShop.Services.Data.Templates
{
    using System;
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
        private readonly IRepository<TemplateSubCategory> templateSubCategoriesRepo;
        private readonly IImagesService imagesService;

        public TemplateService(
            IRepository<Template> templateRepo,
            IRepository<TemplateCategory> templateCategoriesRepo,
            IRepository<TemplateSubCategory> templateSubCategoriesRepo,
            IImagesService imagesService)
        {
            this.templateRepo = templateRepo;
            this.templateCategoriesRepo = templateCategoriesRepo;
            this.templateSubCategoriesRepo = templateSubCategoriesRepo;
            this.imagesService = imagesService;
        }

        public async Task AddAsync(string name, string description, decimal price, IFormFile image, IFormFile secondImage, IFormFile thirdImage, int imagesFixedCount, bool isBaseModel, bool hasCustomText, int? subCategoryId, IEnumerable<int> categoriesIds)
        {
            var categories = await (from categoriesTable in this.templateCategoriesRepo.All()
                                    where categoriesIds.Any(id => categoriesTable.Id.Equals(id))
                                    select categoriesTable).ToListAsync();

            var template = new Template
            {
                Name = name,
                Description = description,
                Price = price,
                ImagesCount = imagesFixedCount,
                SubCategoryId = subCategoryId,
                HasCustomText = hasCustomText,
                IsBaseModel = isBaseModel,
                ImageUrl = await this.imagesService.UploadAsync(image, GlobalConstants.TemplatesFolderName),
                SecondImageUrl = await this.imagesService.UploadAsync(secondImage, GlobalConstants.TemplatesFolderName),
                ThirdImageUrl = await this.imagesService.UploadAsync(thirdImage, GlobalConstants.TemplatesFolderName),
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
                templates = templates.Where(x => x.SubCategoryId.Equals(subCategoryId));
            }

            if (take.HasValue)
            {
                templates = templates.Skip(skip).Take(take.Value);
            }

            return await templates
                .To<TModel>()
                .ToListAsync();
        }

        public async Task CreateCategoryAsync(string name, decimal price, IFormFile imageUrl, IEnumerable<int> tempalteIds)
        {
            var templateCategory = new TemplateCategory
            {
                Name = name,
                Price = price,
                ImageUrl = await this.imagesService.UploadAsync(imageUrl, GlobalConstants.TemplatesFolderName),
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

            this.imagesService.Delete(template.ImageUrl, GlobalConstants.TemplatesFolderName);
            this.imagesService.Delete(template.SecondImageUrl, GlobalConstants.TemplatesFolderName);
            this.imagesService.Delete(template.ThirdImageUrl, GlobalConstants.TemplatesFolderName);

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
                query = query.Where(template => template.SubCategoryId.Equals(subCategoryId.Value));
            }

            return await query.CountAsync();
        }

        public async Task<TModel> GetCategoryAsync<TModel>(int categoryId)
            => await this.templateCategoriesRepo
            .AllAsNoTracking()
            .Where(x => x.Id.Equals(categoryId))
            .To<TModel>()
            .FirstOrDefaultAsync();

        public async Task CreateSubCategoryAsync(string name)
        {
            await this.templateSubCategoriesRepo.AddAsync(new TemplateSubCategory { Name = name });
            await this.templateSubCategoriesRepo.SaveChangesAsync();
        }

        public async Task RemoveSubCategoryAsync(int categoryId)
        {
            var category = await this.templateSubCategoriesRepo
                  .All()
                  .FirstOrDefaultAsync(x => x.Id.Equals(categoryId));

            this.templateSubCategoriesRepo.Delete(category);
            await this.templateSubCategoriesRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<TModel>> GetAllSubCategoriesAsync<TModel>()
            => await this.templateSubCategoriesRepo
            .AllAsNoTracking()
            .To<TModel>()
            .ToListAsync();

        public async Task<IEnumerable<TModel>> GetRandomAsync<TModel>(int take, int categoryId)
            => await this.templateRepo
            .AllAsNoTracking()
            .Where(x => x.TemplateCategories.Any(c => c.Id.Equals(categoryId)))
            .OrderBy(x => Guid.NewGuid())
            .Take(take)
            .To<TModel>()
            .ToListAsync();

        public async Task<TModel> GetRandomCategoryAsync<TModel>()
            => await this.templateCategoriesRepo
            .AllAsNoTracking()
            .OrderBy(x => Guid.NewGuid())
            .To<TModel>()
            .FirstOrDefaultAsync();

        public async Task EditAsync(int id, string name, string description, decimal price, IFormFile image, IFormFile secondImage, IFormFile thirdImage, int imagesFixedCount, bool isBaseModel, bool hasCustomText, int? subCategoryId, IEnumerable<int> categoriesIds)
        {
            var categories = await (from categoriesTable in this.templateCategoriesRepo.All()
                                    where categoriesIds.Any(id => categoriesTable.Id.Equals(id))
                                    select categoriesTable).ToListAsync();

            var template = this.templateRepo
                .All()
                .Include(x => x.TemplateCategories)
                .FirstOrDefault(x => x.Id.Equals(id));

            this.imagesService.Delete(template.ImageUrl, GlobalConstants.TemplatesFolderName);
            this.imagesService.Delete(template.SecondImageUrl, GlobalConstants.TemplatesFolderName);
            this.imagesService.Delete(template.ThirdImageUrl, GlobalConstants.TemplatesFolderName);

            template.ImageUrl = await this.imagesService.UploadAsync(image, GlobalConstants.TemplatesFolderName);
            template.SecondImageUrl = await this.imagesService.UploadAsync(secondImage, GlobalConstants.TemplatesFolderName);
            template.ThirdImageUrl = await this.imagesService.UploadAsync(thirdImage, GlobalConstants.TemplatesFolderName);
            template.Name = name;
            template.Description = description;
            template.Price = price;
            template.ImagesCount = imagesFixedCount;
            template.SubCategoryId = subCategoryId;
            template.HasCustomText = hasCustomText;
            template.IsBaseModel = isBaseModel;
            template.TemplateCategories = categories;

            this.templateRepo.Update(template);
            await this.templateRepo.SaveChangesAsync();
        }

        public async Task EditCategoryImageAsync(int categoryId, IFormFile image)
        {
            var templateCategory = await this.templateCategoriesRepo
                .All()
                .FirstOrDefaultAsync(x => x.Id.Equals(categoryId));

            this.imagesService.Delete(templateCategory.ImageUrl, GlobalConstants.TemplatesFolderName);
            templateCategory.ImageUrl = await this.imagesService.UploadAsync(image, GlobalConstants.TemplatesFolderName);

            this.templateCategoriesRepo.Update(templateCategory);
            await this.templateCategoriesRepo.SaveChangesAsync();
        }
    }
}
