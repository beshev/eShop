namespace EShop.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EShop.Common;
    using EShop.Data.Models.Enums;
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

        public async Task<IActionResult> All(OrderStatus orderStatus = OrderStatus.Active)
        {
            var viewModel = new AllOrdersViewModel
            {
                Orders = await this.ordersService.GetAllAsync<OrderViewModel>(orderStatus),
                Status = orderStatus,
            };

            this.TempData[GlobalConstants.ChangeStatusAction] = orderStatus;

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
            return this.RedirectToAction(nameof(this.All), new { OrderStatus = this.TempData[GlobalConstants.ChangeStatusAction] });
        }

        public async Task<IActionResult> ChangeStatus(int id, OrderStatus orderStatus)
        {
            await this.ordersService.ChangeStatus(id, orderStatus);
            return this.RedirectToAction(nameof(this.All), new { OrderStatus = this.TempData[GlobalConstants.ChangeStatusAction] });
        }
    }
}
