namespace EShop.Services.Data.Products
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IProductService
    {
        public Task AddAsync(string name, decimal price, string description, int categoryId, bool hasCustomText, IFormFile image, IEnumerable<int> templatesIds);

        public Task DeleteByIdAsync(int id);

        public Task<IEnumerable<TModel>> GetAllAsync<TModel>(int? categoryId = null);

        public Task<TModel> GetByIdAsync<TModel>(int id);

        public Task CreateCategoryAsync(string name);

        public Task<IEnumerable<TModel>> GetAllWithTemplatesAsync<TModel>();

        public Task<IEnumerable<TModel>> GetCategoriesAsync<TModel>();

        Task RemoveCategoryAsync(int categoryId);
    }
}
