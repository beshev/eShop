namespace EShop.Web.ViewComponents
{
    using EShop.Services.Data.Products;
    using EShop.Services.Data.Templates;
    using EShop.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesSelectViewComponent : ViewComponent
    {
        private readonly IProductService productService;
        private readonly ITemplateService templateService;

        public CategoriesSelectViewComponent(
            IProductService productService,
            ITemplateService templateService)
        {
            this.productService = productService;
            this.templateService = templateService;
        }

        public IViewComponentResult Invoke(bool isForProducts)
        {
            var viewModel = isForProducts
                ? this.productService.GetCategoriesAsync<SelectViewModel>().GetAwaiter().GetResult()
                : this.templateService.GetCategoriesAsync<SelectViewModel>().GetAwaiter().GetResult();

            return this.View(viewModel);
        }
    }
}
