namespace EShop.Services.Data.Templates
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using EShop.Data.Common.Repositories;
    using EShop.Data.Models;
    using Microsoft.AspNetCore.Http;

    public class TemplateService : ITemplateService
    {
        private readonly IRepository<Template> templateRepo;

        public TemplateService(IRepository<Template> templateRepo)
        {
            this.templateRepo = templateRepo;
        }

        public async Task AddAsync(string name, string description, decimal price, IFormFile image, int imagesFixedCount, bool isBaseModel, bool hasCustomText, int templateCategoryId, IEnumerable<int> productsIds)
        {
            var template = new Template
            {
                Name = name,
                Description = description,
                Price = price,
                ImagesFixedCount = imagesFixedCount,
                TemplateCategoryId = templateCategoryId,
                HasCustomText = hasCustomText,
                IsBaseModel = isBaseModel,
                Base64Image = ConvertIFormFileToBase64(image),
                TemplateProducts = productsIds.Select(productId => new ProductTemplate
                {
                    ProdcutId = productId,
                }).ToList(),
            };

            await this.templateRepo.AddAsync(template);
            await this.templateRepo.SaveChangesAsync();
        }

        private static string ConvertIFormFileToBase64(IFormFile file)
        {
            if (file.Length == 0)
            {
                return null;
            }

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                return Convert.ToBase64String(fileBytes);
            }
        }
    }
}
