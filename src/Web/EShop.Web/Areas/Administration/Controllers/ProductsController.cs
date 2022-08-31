namespace EShop.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EShop.Common;
    using EShop.Services.Data.Products;
    using EShop.Web.Infrastructure.Attributes;
    using EShop.Web.ViewModels;
    using EShop.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : AdministrationController
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> All()
        {
            var model = await this.productService.GetAllAsync<ProductViewModel>();

            return this.View(model);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductInputModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            try
            {
                await this.productService.AddAsync(model.Name, model.Price, model.Description, model.CategoryId, model.HasCustomText, model.Image, model.TemplatesIds);
                return this.RedirectToAction(nameof(this.All));
            }
            catch (Exception)
            {
                return this.RedirectToAction(GlobalConstants.ErrorAction, GlobalConstants.HomeController, new { Area = string.Empty });
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.productService.DeleteByIdAsync(id);
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
                await this.productService.CreateCategoryAsync(model.CategoryName);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> RemoveCategory(int categoryId)
        {
            try
            {
                await this.productService.RemoveCategoryAsync(categoryId);
                return this.RedirectToAction(nameof(this.All));
            }
            catch (Exception)
            {
                return this.RedirectToAction(GlobalConstants.ErrorAction, GlobalConstants.HomeController, new { Area = string.Empty });
            }
        }
    }
}
