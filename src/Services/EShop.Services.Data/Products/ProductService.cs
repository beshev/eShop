namespace EShop.Services.Data.Products
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

    public class ProductService : IProductService
    {
        private readonly IRepository<Product> productRepo;
        private readonly IRepository<ProductCategory> productCategoryRepo;
        private readonly IImagesService imagesService;

        public ProductService(
            IRepository<Product> productRepo,
            IRepository<ProductCategory> productCategoryRepo,
            IImagesService imagesService)
        {
            this.productRepo = productRepo;
            this.productCategoryRepo = productCategoryRepo;
            this.imagesService = imagesService;
        }

        public async Task AddAsync(string name, decimal price, string description, int categoryId, bool hasCustomText, bool hasFontStyle, IFormFile image, IFormFile secondImage, IFormFile thirdImage, int imagesCount)
        {
            var prodcut = new Product
            {
                Name = name,
                Price = price,
                Description = description,
                HasCustomText = hasCustomText,
                HasFontStyle = hasFontStyle,
                ImagesCount = imagesCount,
                ProductCategoryId = categoryId,
                ImageUrl = await this.imagesService.UploadAsync(image, GlobalConstants.ProductsFolderName),
                SecondImageUrl = await this.imagesService.UploadAsync(secondImage, GlobalConstants.ProductsFolderName),
                ThirdImageUrl = await this.imagesService.UploadAsync(thirdImage, GlobalConstants.ProductsFolderName),
            };

            await this.productRepo.AddAsync(prodcut);
            await this.productRepo.SaveChangesAsync();
        }

        public async Task ChangeStatus(int id)
        {
            var product = await this.productRepo
                .All()
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            product.IsOutOfStock = !product.IsOutOfStock;

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

            this.imagesService.Delete(product.ImageUrl, GlobalConstants.ProductsFolderName);
            this.imagesService.Delete(product.SecondImageUrl, GlobalConstants.ProductsFolderName);
            this.imagesService.Delete(product.ThirdImageUrl, GlobalConstants.ProductsFolderName);

            this.productRepo.Delete(product);
            await this.productRepo.SaveChangesAsync();
        }

        public async Task EditAsync(int id, string name, decimal price, string description, int categoryId, bool hasCustomText, bool hasFontStyle, IFormFile image, IFormFile secondImage, IFormFile thirdImage, int imagesCount)
        {
            var product = this.productRepo
                   .All()
                   .FirstOrDefault(x => x.Id.Equals(id));

            this.imagesService.Delete(product.ImageUrl, GlobalConstants.ProductsFolderName);
            this.imagesService.Delete(product.SecondImageUrl, GlobalConstants.ProductsFolderName);
            this.imagesService.Delete(product.ThirdImageUrl, GlobalConstants.ProductsFolderName);

            product.ImageUrl = await this.imagesService.UploadAsync(image, GlobalConstants.ProductsFolderName);
            product.SecondImageUrl = await this.imagesService.UploadAsync(secondImage, GlobalConstants.ProductsFolderName);
            product.ThirdImageUrl = await this.imagesService.UploadAsync(thirdImage, GlobalConstants.ProductsFolderName);
            product.Name = name;
            product.Description = description;
            product.Price = price;
            product.ImagesCount = imagesCount;
            product.HasCustomText = hasCustomText;
            product.HasFontStyle = hasFontStyle;
            product.ProductCategoryId = categoryId;

            this.productRepo.Update(product);
            await this.productRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<TModel>> GetAllAsync<TModel>(int skip = 0, int? take = null, int? categoryId = null)
        {
            var products = this.productRepo.AllAsNoTracking();

            if (categoryId.HasValue)
            {
                products = products.Where(x => x.ProductCategoryId.Equals(categoryId));
            }

            if (take.HasValue)
            {
                products = products.Skip(skip).Take(take.Value);
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

        public async Task<int> GetCountAsync(int? categoryId = null)
        {
            var query = this.productRepo
                .AllAsNoTracking();

            if (categoryId.HasValue)
            {
                query = query.Where(x => x.ProductCategoryId.Equals(categoryId.Value));
            }

            return await query.CountAsync();
        }

        public async Task<IEnumerable<TModel>> GetRandomAsync<TModel>(int count, bool outOfStockFilter = false)
        {
            var products = this.productRepo
                .AllAsNoTracking();

            if (outOfStockFilter)
            {
                products = products.Where(x => x.IsOutOfStock == false);
            }

            return await products
                .OrderBy(x => Guid.NewGuid())
                .Take(count)
                .To<TModel>()
                .ToListAsync();
        }

        public async Task RemoveCategoryAsync(int categoryId)
        {
            var category = await this.productCategoryRepo
                .All()
                .FirstOrDefaultAsync(x => x.Id.Equals(categoryId));

            this.productCategoryRepo.Delete(category);
            await this.productCategoryRepo.SaveChangesAsync();
        }
    }
}
