namespace EShop.Web.ViewComponents
{
    using EShop.Services.Data.Products;
    using EShop.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class ProductCategoriesSelectViewComponent : ViewComponent
    {
        private readonly IProductService productService;

        public ProductCategoriesSelectViewComponent(IProductService productService)
        {
            this.productService = productService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = this.productService
                .GetCategoriesAsync<SelectViewModel>()
                .GetAwaiter()
                .GetResult();

            return this.View(viewModel);
        }
    }
}
