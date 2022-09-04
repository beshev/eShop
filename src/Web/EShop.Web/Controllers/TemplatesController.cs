namespace EShop.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EShop.Common;
    using EShop.Services.Data.Templates;
    using EShop.Web.ViewModels.Templates;
    using Microsoft.AspNetCore.Mvc;

    public class TemplatesController : BaseController
    {
        private const int TemplatesPerPage = 12;
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

            if (viewModel.Templates.Any() == false)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int templateId, int categoryId)
        {
            if (await this.templateService.IsCompatibleWithProductAsync(templateId, categoryId) == false)
            {
                return this.NotFound();
            }

            var viewModel = await this.templateService.GetByIdAsync<TemplateDetailsModel>(templateId);
            viewModel.Category = await this.templateService.GetCategoryAsync<TemplateCategoryViewModel>(categoryId);

            // TODO - This is for paginaton chek if there is a way to removed it
            viewModel.CategoryId = categoryId;

            this.ViewData[GlobalConstants.ReturnUrlKey] = this.ReturnUrl;

            return this.View(viewModel);
        }
    }
}
