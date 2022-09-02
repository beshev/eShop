namespace EShop.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EShop.Common;
    using EShop.Services.Data.Products;
    using EShop.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;

    public class Products : BaseController
    {
        private const int ProductsPerPage = 12;
        private readonly IProductService productService;

        public Products(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> All(int categoryId, int id = 1)
        {
            if (id < 0)
            {
                return this.NotFound();
            }

            int count = await this.productService.GetCountAsync(categoryId);
            int pagesCount = (int)Math.Ceiling((double)count / ProductsPerPage);

            if (pagesCount != 0 && id > pagesCount)
            {
                return this.NotFound();
            }

            try
            {
                var skip = (id - 1) * ProductsPerPage;
                var viewModel = new AllProductsViewModel
                {
                    PageNumber = id,
                    PagesCount = pagesCount,
                    ForAction = nameof(this.All),
                    ForController = this.GetType().Name.Replace(nameof(Controller), string.Empty),
                    Products = await this.productService.GetAllAsync<ProductViewModel>(categoryId),
                };

                if (viewModel.Products.Any() == false)
                {
                    return this.NotFound();
                }

                return this.View(viewModel);
            }
            catch (Exception)
            {
                return this.RedirectToAction(GlobalConstants.ErrorAction, GlobalConstants.HomeController);
            }
        }

        public async Task<IActionResult> Details(int productId)
        {
            var vieModel = await this.productService.GetByIdAsync<ProductDetailsModel>(productId);

            this.ViewData[GlobalConstants.ReturnUrlKey] = this.ReturnUrl;

            this.TempData[GlobalConstants.NameOfOrderProductId] = productId;

            return this.View(vieModel);
        }
    }
}
