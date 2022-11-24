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
                ImageUrl = await this.cloudinaryService.UploadAsync(image, string.Format(GlobalConstants.ProductCloundFolderName, name, 1)),
                SecondImageUrl = await this.cloudinaryService.UploadAsync(secondImage, string.Format(GlobalConstants.ProductCloundFolderName, name, 2)),
                ThirdImageUrl = await this.cloudinaryService.UploadAsync(thirdImage, string.Format(GlobalConstants.ProductCloundFolderName, name, 3)),
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

            await Task.WhenAll(
                this.cloudinaryService.DeleteAsync(string.Format(GlobalConstants.ProductCloundFolderName, product.Name, 1)),
                this.cloudinaryService.DeleteAsync(string.Format(GlobalConstants.ProductCloundFolderName, product.Name, 2)),
                this.cloudinaryService.DeleteAsync(string.Format(GlobalConstants.ProductCloundFolderName, product.Name, 3)));

            this.productRepo.Delete(product);
            await this.productRepo.SaveChangesAsync();
        }

        public async Task EditAsync(int id, string name, decimal price, string description, int categoryId, bool hasCustomText, bool hasFontStyle, IFormFile image, IFormFile secondImage, IFormFile thirdImage, int imagesCount)
        {
            var product = this.productRepo
                   .All()
                   .FirstOrDefault(x => x.Id.Equals(id));

            product.ImageUrl = await this.GetImageUrlAsync(product.ImageUrl, string.Format(GlobalConstants.ProductCloundFolderName, product.Name, 1), string.Format(GlobalConstants.ProductCloundFolderName, name, 1), image);
            product.SecondImageUrl = await this.GetImageUrlAsync(product.SecondImageUrl, string.Format(GlobalConstants.ProductCloundFolderName, product.Name, 2), string.Format(GlobalConstants.ProductCloundFolderName, name, 2), secondImage);
            product.ThirdImageUrl = await this.GetImageUrlAsync(product.ThirdImageUrl, string.Format(GlobalConstants.ProductCloundFolderName, product.Name, 3), string.Format(GlobalConstants.ProductCloundFolderName, name, 3), thirdImage);
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
                query = query.Where(x => x.Id.Equals(categoryId.Value));
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
