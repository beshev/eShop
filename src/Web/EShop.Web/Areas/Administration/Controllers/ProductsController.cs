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
        private const int ProductsPerPage = 12;

        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> All(int id = 1)
        {
            int count = await this.productService.GetCountAsync();
            int pagesCount = (int)Math.Ceiling((double)count / ProductsPerPage);

            var skip = (id - 1) * ProductsPerPage;
            var viewModel = new AllProductsViewModel
            {
                Area = GlobalConstants.AdministrationArea,
                PageNumber = id,
                PagesCount = pagesCount,
                ForAction = nameof(this.All),
                ForController = this.GetType().Name.Replace(nameof(Controller), string.Empty),
                Products = await this.productService.GetAllAsync<ProductViewModel>(skip, ProductsPerPage),
            };

            return this.View(viewModel);
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

            await this.productService.AddAsync(model.Name, model.Price, model.Description, model.CategoryId, model.HasCustomText, model.HasFontStyle, model.Image, model.SecondImage, model.ThirdImage, model.ImagesCount);
            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var template = await this.productService.GetByIdAsync<ProductEditModel>(id);
            return this.View(template);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductEditModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            await this.productService.EditAsync(model.Id, model.Name, model.Price, model.Description, model.CategoryId, model.HasCustomText, model.HasFontStyle, model.Image, model.SecondImage, model.ThirdImage, model.ImagesCount);
            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.productService.DeleteByIdAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> ChangeStatus(int id)
        {
            await this.productService.ChangeStatus(id);
            return this.RedirectToAction(nameof(this.All));
        }

        [SetTempDataErrorsAttribute(GlobalConstants.NameOfCategory)]
        public async Task<IActionResult> AddCategory(CategoryInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.productService.CreateCategoryAsync(model.Name);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> RemoveCategory(int categoryId)
        {
            await this.productService.RemoveCategoryAsync(categoryId);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
