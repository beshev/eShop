namespace EShop.Web.ViewComponents
{
    using EShop.Services.Data.Products;
    using EShop.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;

    public class ProductSelectViewComponent : ViewComponent
    {
        private readonly IProductService productService;

        public ProductSelectViewComponent(IProductService productService)
        {
            this.productService = productService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = this.productService
                .GetAllAsync<ProductSelectModel>()
                .GetAwaiter()
                .GetResult();

            return this.View(viewModel);
        }
    }
}
