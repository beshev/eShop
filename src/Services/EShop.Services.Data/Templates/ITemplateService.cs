namespace EShop.Services.Data.Templates
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ITemplateService
    {
        public Task<IEnumerable<TModel>> GetAllAsync<TModel>(int? productId = null, int? category = null, int skip = 0, int? take = null);

        public Task<IEnumerable<TModel>> GetCategoriesAsync<TModel>();

        public Task CreateCategoryAsync(string name, decimal price, IEnumerable<int> tempalteIds);

        public Task RemoveCategoryAsync(int categoryId);

        public Task AddAsync(string name, string description, decimal price, IFormFile base64Imiges, int imagesFixedCount, bool isBaseModel, bool hasCustomText, int templateCategoryId, IEnumerable<int> productsIds);

        public Task<TModel> GetByIdAsync<TModel>(int id);

        public Task DeleteByIdAsync(int id);

        public Task<bool> IsCompatibleWithProductAsync(int templateId, int productId);

        public Task<int> GetCountAsync(int? productId = null, int? categoryId = null);

        public Task<TModel> GetCategoryAsync<TModel>(int categoryId);
    }
}
