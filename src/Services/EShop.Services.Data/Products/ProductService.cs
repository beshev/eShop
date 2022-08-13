namespace EShop.Services.Data.Products
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

    public class ProductService : IProductService
    {
        private readonly IRepository<Product> productRepo;
        private readonly ICloudinaryService cloudinaryService;

        public ProductService(
            IRepository<Product> productRepo,
            ICloudinaryService cloudinaryService)
        {
            this.productRepo = productRepo;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task AddAsync(string name, decimal price, string description, int categoryId, bool hasCustomText, IFormFile image)
        {
            var cloudFolder = $"products/{name}";

            var prodcut = new Product
            {
                Name = name,
                Price = price,
                Description = description,
                HasCustomText = hasCustomText,
                ProductCategoryId = categoryId,
                ImageUrl = await this.cloudinaryService.UploadAsync(image, cloudFolder),
            };

            await this.productRepo.AddAsync(prodcut);
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
    }
}
