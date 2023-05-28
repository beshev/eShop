namespace EShop.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EShop.Common;
    using EShop.Services.Data.Templates;
    using EShop.Web.ViewModels.Templates;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    [ResponseCache(CacheProfileName = GlobalConstants.ItemsCacheProfileName)]
    public class TemplatesController : BaseController
    {
        private const int TemplatesPerPage = 16;
        private readonly ITemplateService templateService;
        private readonly IMemoryCache memoryCache;

        public TemplatesController(
            ITemplateService templateService,
            IMemoryCache memoryCache)
        {
            this.templateService = templateService;
            this.memoryCache = memoryCache;
        }

        public async Task<IActionResult> All(int id = 1, int? categoryId = null, int? subCategoryId = null)
        {
            if (categoryId.HasValue == false)
            {
                return this.NotFound();
            }

            if (id < 1)
            {
                return this.NotFound();
            }

            int count = await this.templateService.GetCountAsync(categoryId, subCategoryId);
            int pagesCount = (int)Math.Ceiling((double)count / TemplatesPerPage);

            if (pagesCount != 0 && id > pagesCount)
            {
                return this.NotFound();
            }

            var skip = (id - 1) * TemplatesPerPage;
            var viewModel = new AllTemplatesViewModel
            {
                CategoryId = categoryId,
                Category = await this.templateService.GetCategoryAsync<TemplateCategoryViewModel>(categoryId.Value),
                PageNumber = id,
                PagesCount = pagesCount,
                SubCategoryId = subCategoryId,
                ForAction = nameof(this.All),
                ForController = this.GetType().Name.Replace(nameof(Controller), string.Empty),
                Templates = await this.templateService.GetAllAsync<TemplateBaseViewModel>(categoryId, subCategoryId, skip, TemplatesPerPage),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int templateId, int categoryId)
        {
            if (await this.templateService.IsCompatibleWithProductAsync(templateId, categoryId) == false)
            {
                return this.BadRequest();
            }

            var templateCacheKey = string.Format(GlobalConstants.TemplateCacheKey, templateId, categoryId);
            if (!this.memoryCache.TryGetValue(templateCacheKey, out TemplateDetailsModel viewModel))
            {
                viewModel = await this.templateService.GetByIdAsync<TemplateDetailsModel>(templateId);
                viewModel.Category = await this.templateService.GetCategoryAsync<TemplateCategoryViewModel>(categoryId);

                this.memoryCache.Set(templateCacheKey, viewModel, TimeSpan.FromSeconds(GlobalConstants.CacheExpirationTimeInSeconds));
            }

            this.ViewData[GlobalConstants.ReturnUrlKey] = this.ReturnUrl;

            return this.View(viewModel);
        }
    }
}
