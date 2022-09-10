namespace EShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using EShop.Services.Data.Orders;
    using EShop.Services.Data.Products;
    using EShop.Services.Data.Templates;
    using EShop.Web.ViewModels.Administration.Dashboard;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly ITemplateService templateService;
        private readonly IProductService productService;
        private readonly IOrdersService ordersService;

        public DashboardController(
            ITemplateService templateService,
            IProductService productService,
            IOrdersService ordersService)
        {
            this.templateService = templateService;
            this.productService = productService;
            this.ordersService = ordersService;
        }

        public async Task<IActionResult> Index()
        {
            var vewModel = new IndexViewModel
            {
                TemplatesCount = await this.templateService.GetCountAsync(),
                ProductsCount = await this.productService.GetCountAsync(null),
                OrdersCount = await this.ordersService.GetCountAsync(),
            };

            return this.View(vewModel);
        }
    }
}
