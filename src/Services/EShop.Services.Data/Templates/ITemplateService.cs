namespace EShop.Services.Data.Templates
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ITemplateService
    {
        public Task AddAsync(string name, string description, decimal price, IFormFile base64Imiges, int imagesFixedCount, bool isBaseModel, bool hasCustomText, int templateCategoryId, IEnumerable<int> productsIds);
    }
}
