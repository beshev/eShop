namespace EShop.Services.Data.Products
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IProductService
    {
        public Task AddAsync(string name, decimal price, string description, int categoryId, bool hasCustomText, bool hasFontStyle, IFormFile image, IFormFile secondImage, IFormFile thirdImage, int imagesCount);

        public Task EditAsync(int id, string name, decimal price, string description, int categoryId, bool hasCustomText, bool hasFontStyle, IFormFile image, IFormFile secondImage, IFormFile thirdImage, int imagesCount);

        public Task DeleteByIdAsync(int id);

        public Task ChangeStatus(int id);

        public Task<IEnumerable<TModel>> GetAllAsync<TModel>(int skip = 0, int? take = null, int? categoryId = null);

        public Task<IEnumerable<TModel>> GetRandomAsync<TModel>(int count, bool outOfStockFilter = false);

        public Task<TModel> GetByIdAsync<TModel>(int id);

        public Task CreateCategoryAsync(string name);

        public Task<IEnumerable<TModel>> GetCategoriesAsync<TModel>();

        public Task<int> GetCountAsync(int? categoryId = null);

        public Task RemoveCategoryAsync(int categoryId);
    }
}
