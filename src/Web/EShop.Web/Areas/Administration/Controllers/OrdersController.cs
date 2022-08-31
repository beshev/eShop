namespace EShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using EShop.Services.Data.Orders;
    using EShop.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : AdministrationController
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        public async Task<IActionResult> All()
        {
            var viewModel = await this.ordersService.GetAllAsync<OrderViewModel>();
            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await this.ordersService.GetByIdAsync<OrderDetailsModel>(id);

            return this.View(viewModel);
        }

        public async Task<IActionResult> Remove(int id)
        {
            await this.ordersService.DeleteByIdAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
