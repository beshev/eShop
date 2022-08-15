namespace EShop.Services.Data.Products
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

    public class ProductService : IProductService
    {
        private readonly IRepository<Product> productRepo;
        private readonly IRepository<ProductCategory> productCategoryRepo;
        private readonly ICloudinaryService cloudinaryService;

        public ProductService(
            IRepository<Product> productRepo,
            IRepository<ProductCategory> productCategoryRepo,
            ICloudinaryService cloudinaryService)
        {
            this.productRepo = productRepo;
            this.productCategoryRepo = productCategoryRepo;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task AddAsync(string name, decimal price, string description, int categoryId, bool hasCustomText, IFormFile image, IEnumerable<int> templatesIds)
        {
            var prodcut = new Product
            {
                Name = name,
                Price = price,
                Description = description,
                HasCustomText = hasCustomText,
                ProductCategoryId = categoryId,
                ImageUrl = await this.cloudinaryService.UploadAsync(image, string.Format(GlobalConstants.ProductCloundFolderName, name)),
                ProductTemplates = templatesIds.Select(templateId => new ProductTemplate { TemplateId = templateId }).ToList(),
            };

            await this.productRepo.AddAsync(prodcut);
            await this.productRepo.SaveChangesAsync();
        }

        public async Task CreateCategoryAsync(string name)
        {
            await this.productCategoryRepo.AddAsync(new ProductCategory { Name = name });
            await this.productCategoryRepo.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var product = await this.productRepo
                .All()
                .FirstOrDefaultAsync(product => product.Id.Equals(id));

            await this.RemoveProductTemplatesAsync(id);
            await this.cloudinaryService.DeleteAsync(string.Format(GlobalConstants.ProductCloundFolderName, product.Name));
            this.productRepo.Delete(product);
            await this.productRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<TModel>> GetAllAsync<TModel>(int? categoryId = null)
        {
            var products = this.productRepo.AllAsNoTracking();

            if (categoryId.HasValue)
            {
                products = products.Where(x => x.ProductCategoryId.Equals(categoryId));
            }

            return await products.To<TModel>().ToListAsync();
        }

        public async Task<TModel> GetByIdAsync<TModel>(int id)
         => await this.productRepo
            .AllAsNoTracking()
            .Where(x => x.Id.Equals(id))
            .To<TModel>()
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<TModel>> GetCategoriesAsync<TModel>()
        => await this.productCategoryRepo
            .AllAsNoTracking()
            .To<TModel>()
            .ToListAsync();

        public async Task UpdateAsync(int id, string name, decimal price, string description, int categoryId, bool hasCustomText, IFormFile image, IEnumerable<int> templatesIds)
        {
            var product = await this.productRepo
                .All()
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            await this.RemoveProductTemplatesAsync(id);

            product.Name = name;
            product.Price = price;
            product.Description = description;
            product.ProductCategoryId = categoryId;
            product.HasCustomText = hasCustomText;
            product.ImageUrl = await this.cloudinaryService.UploadAsync(image, string.Format(GlobalConstants.ProductCloundFolderName, name));
            product.ProductTemplates = templatesIds.Select(templateId => new ProductTemplate { TemplateId = templateId }).ToList();

            this.productRepo.Update(product);
            await this.productRepo.SaveChangesAsync();
        }

        private async Task RemoveProductTemplatesAsync(int productId)
        {
            await this.productRepo.SqlRawAsync(GlobalConstants.DeleteFromProductsTemplatesTableQuery, productId);
        }
    }
}
