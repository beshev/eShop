namespace EShop.Services.Data.Products
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IProductService
    {
        public Task AddAsync(string name, decimal price, string description, int categoryId, bool hasCustomText, bool hasFontStyle, IFormFile image, int imagesCount);

        public Task DeleteByIdAsync(int id);

        public Task ChangeStatus(int id);

        public Task<IEnumerable<TModel>> GetAllAsync<TModel>(int? categoryId = null);

        public Task<IEnumerable<TModel>> GetRandomAsync<TModel>(int count);

        public Task<TModel> GetByIdAsync<TModel>(int id);

        public Task CreateCategoryAsync(string name);

        public Task<IEnumerable<TModel>> GetCategoriesAsync<TModel>();

        public Task<int> GetCountAsync(int? categoryId);

        public Task RemoveCategoryAsync(int categoryId);
    }
}
