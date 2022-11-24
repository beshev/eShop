namespace EShop.Services.Data.Templates
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
        private readonly ICloudinaryService cloudinaryService;

        public TemplateService(
            IRepository<Template> templateRepo,
            IRepository<TemplateCategory> templateCategoriesRepo,
            IRepository<TemplateSubCategory> templateSubCategoriesRepo,
            ICloudinaryService cloudinaryService)
        {
            this.templateRepo = templateRepo;
            this.templateCategoriesRepo = templateCategoriesRepo;
            this.templateSubCategoriesRepo = templateSubCategoriesRepo;
            this.cloudinaryService = cloudinaryService;
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
                ImageUrl = await this.cloudinaryService.UploadAsync(image, string.Format(GlobalConstants.TemplateCloundFolderName, name, 1)),
                SecondImageUrl = await this.cloudinaryService.UploadAsync(secondImage, string.Format(GlobalConstants.TemplateCloundFolderName, name, 2)),
                ThirdImageUrl = await this.cloudinaryService.UploadAsync(thirdImage, string.Format(GlobalConstants.TemplateCloundFolderName, name, 3)),
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
                ImageUrl = await this.cloudinaryService.UploadAsync(imageUrl, string.Format(GlobalConstants.TemplateCloundFolderName, name, 1)),
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

            await Task.WhenAll(
                this.cloudinaryService.DeleteAsync(string.Format(GlobalConstants.TemplateCloundFolderName, template.Name, 1)),
                this.cloudinaryService.DeleteAsync(string.Format(GlobalConstants.TemplateCloundFolderName, template.Name, 2)),
                this.cloudinaryService.DeleteAsync(string.Format(GlobalConstants.TemplateCloundFolderName, template.Name, 3)));

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

            if (secondImage is not null)
            {
                await this.cloudinaryService.DeleteAsync(string.Format(GlobalConstants.TemplateCloundFolderName, template.Name, 2));
                template.SecondImageUrl = await this.cloudinaryService.UploadAsync(secondImage, string.Format(GlobalConstants.TemplateCloundFolderName, name, 2));
            }

            if (thirdImage is not null)
            {
                await this.cloudinaryService.DeleteAsync(string.Format(GlobalConstants.TemplateCloundFolderName, template.Name, 3));
                template.ThirdImageUrl = await this.cloudinaryService.UploadAsync(thirdImage, string.Format(GlobalConstants.TemplateCloundFolderName, name, 3));
            }

            template.ImageUrl = await this.GetImageUrlAsync(template.ImageUrl, string.Format(GlobalConstants.TemplateCloundFolderName, template.Name, 1), string.Format(GlobalConstants.TemplateCloundFolderName, name, 1), image);
            template.SecondImageUrl = await this.GetImageUrlAsync(template.SecondImageUrl, string.Format(GlobalConstants.TemplateCloundFolderName, template.Name, 2), string.Format(GlobalConstants.TemplateCloundFolderName, name, 2), secondImage);
            template.ThirdImageUrl = await this.GetImageUrlAsync(template.ThirdImageUrl, string.Format(GlobalConstants.TemplateCloundFolderName, template.Name, 3), string.Format(GlobalConstants.TemplateCloundFolderName, name, 3), thirdImage);
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

        private async Task<string> GetImageUrlAsync(string currentImageUrl, string oldPath, string newPath, IFormFile image)
        {
            var resultUrl = currentImageUrl;
            if (oldPath.Equals(newPath, StringComparison.CurrentCultureIgnoreCase) && image is not null)
            {
                resultUrl = await this.cloudinaryService.UploadAsync(image, oldPath);
            }
            else if ((oldPath.Equals(newPath, StringComparison.CurrentCultureIgnoreCase) == false) && image is null)
            {
                await this.cloudinaryService.RenameAsync(oldPath, newPath);
            }
            else if ((oldPath.Equals(newPath, StringComparison.CurrentCultureIgnoreCase) == false) && image is not null)
            {
                await this.cloudinaryService.DeleteAsync(oldPath);
                resultUrl = await this.cloudinaryService.UploadAsync(image, newPath);
            }

            return resultUrl;
        }
    }
}
