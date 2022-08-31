namespace EShop.Web.ViewComponents
{
    using EShop.Services.Data.Products;
    using EShop.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;

    public class ProductWithTemplatesViewComponent : ViewComponent
    {
        private readonly IProductService productService;

        public ProductWithTemplatesViewComponent(IProductService productService)
        {
            this.productService = productService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = this.productService
                .GetAllWithTemplatesAsync<ProductSelectModel>()
                .GetAwaiter()
                .GetResult();

            return this.View(viewModel);
        }
    }
}
