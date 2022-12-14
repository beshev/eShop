namespace EShop.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EShop.Common;
    using EShop.Services.Data.Templates;
    using EShop.Web.ViewModels.Templates;
    using Microsoft.AspNetCore.Mvc;

    public class TemplatesController : BaseController
    {
        private const int TemplatesPerPage = 16;
        private readonly ITemplateService templateService;

        public TemplatesController(
            ITemplateService templateService)
        {
            this.templateService = templateService;
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

            var viewModel = await this.templateService.GetByIdAsync<TemplateDetailsModel>(templateId);
            viewModel.Category = await this.templateService.GetCategoryAsync<TemplateCategoryViewModel>(categoryId);

            this.ViewData[GlobalConstants.ReturnUrlKey] = this.ReturnUrl;

            return this.View(viewModel);
        }
    }
}
