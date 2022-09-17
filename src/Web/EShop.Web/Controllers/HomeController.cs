namespace EShop.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using EShop.Services.Data.Templates;
    using EShop.Web.ViewModels;
    using EShop.Web.ViewModels.Home;
    using EShop.Web.ViewModels.Templates;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ITemplateService templateService;

        public HomeController(ITemplateService templateService)
        {
            this.templateService = templateService;
        }

        public async Task<IActionResult> Index()
        {
            var templateCategoryId = await this.templateService.GetRandomCategoryIdAsync();
            var viewModel = new IndexViewModel
            {
                TemplateCategoryId = templateCategoryId,
                Templates = await this.templateService.GetRandomAsync<TemplateBaseViewModel>(6, templateCategoryId),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
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
