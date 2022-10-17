namespace EShop.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using EShop.Services.Data.Products;
    using EShop.Services.Data.Templates;
    using EShop.Web.ViewModels;
    using EShop.Web.ViewModels.Home;
    using EShop.Web.ViewModels.Products;
    using EShop.Web.ViewModels.Templates;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ITemplateService templateService;
        private readonly IProductService productService;

        public HomeController(
            ITemplateService templateService,
            IProductService productService)
        {
            this.templateService = templateService;
            this.productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var templateCategorySelectModel = await this.templateService.GetRandomCategoryAsync<TemplateCategoryViewModel>();
            var viewModel = new IndexViewModel
            {
                TemplateCategoryPrice = templateCategorySelectModel.Price,
                TemplateCategoryName = templateCategorySelectModel.Name,
                TemplateCategoryId = templateCategorySelectModel.Id,
                Templates = await this.templateService.GetRandomAsync<TemplateBaseViewModel>(8, templateCategorySelectModel.Id),
                Products = await this.productService.GetRandomAsync<ProductViewModel>(8, true),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult GeneralTerms()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
