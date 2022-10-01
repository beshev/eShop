namespace EShop.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EShop.Common;
    using EShop.Services.Data.Templates;
    using EShop.Web.Infrastructure.Attributes;
    using EShop.Web.ViewModels;
    using EShop.Web.ViewModels.Templates;
    using Microsoft.AspNetCore.Mvc;

    public class TemplatesController : AdministrationController
    {
        private const int TemplatesPerPage = 12;

        private readonly ITemplateService templateService;

        public TemplatesController(ITemplateService templateService)
        {
            this.templateService = templateService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(TemplateInputModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            await this.templateService.AddAsync(model.Name, model.Description, model.Price, model.Image, model.ImagesFixedCount, model.IsBaseModel, model.HasCustomText, model.SubCategoryId, model.CategoriesIds);
            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> All(int id = 1)
        {
            int count = await this.templateService.GetCountAsync();
            int pagesCount = (int)Math.Ceiling((double)count / TemplatesPerPage);

            var skip = (id - 1) * TemplatesPerPage;
            var viewModel = new AllTemplatesViewModel
            {
                Area = GlobalConstants.AdministrationArea,
                PageNumber = id,
                PagesCount = pagesCount,
                ForAction = nameof(this.All),
                ForController = this.GetType().Name.Replace(nameof(Controller), string.Empty),
                Templates = await this.templateService.GetAllAsync<TemplateBaseViewModel>(null, null, skip, TemplatesPerPage),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.templateService.DeleteByIdAsync(id);
                return this.RedirectToAction(nameof(this.All));
            }
            catch (Exception)
            {
                return this.RedirectToAction(GlobalConstants.ErrorAction, GlobalConstants.HomeController, new { Area = string.Empty });
            }
        }

        [SetTempDataErrorsAttribute(GlobalConstants.NameOfCategory)]
        public async Task<IActionResult> AddCategory(CategoryInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.templateService.CreateCategoryAsync(model.Name, model.Price, model.Image, model.TemplatesIds);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> RemoveCategory(int categoryId)
        {
            await this.templateService.RemoveCategoryAsync(categoryId);
            return this.RedirectToAction(nameof(this.All));
        }

        [SetTempDataErrorsAttribute(GlobalConstants.NameOfSubCategory)]
        public async Task<IActionResult> AddSubCategory(CategoryInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.templateService.CreateSubCategoryAsync(model.Name);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> RemoveSubCategory(int categoryId)
        {
            await this.templateService.RemoveSubCategoryAsync(categoryId);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
