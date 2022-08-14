namespace EShop.Services.Data.Products
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IProductService
    {
        public Task AddAsync(string name, decimal price, string description, int categoryId, bool hasCustomText, IFormFile image);

        public Task<IEnumerable<TModel>> GetAllAsync<TModel>(int? categoryId = null);

        public Task CreateCategoryAsync(string name);
    }
}
