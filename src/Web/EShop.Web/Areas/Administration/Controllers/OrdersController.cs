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
            try
            {
                var viewModel = new AllOrdersViewModel
                {
                    Orders = await this.ordersService.GetAllAsync<OrderViewModel>(orderStatus),
                    Status = orderStatus,
                };

                this.TempData[GlobalConstants.ChangeStatusAction] = orderStatus;

                return this.View(viewModel);
            }
            catch (Exception)
            {
                return this.RedirectToAction(GlobalConstants.ErrorAction, GlobalConstants.HomeController, new { Area = string.Empty });
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var viewModel = await this.ordersService.GetByIdAsync<OrderDetailsModel>(id);
                return this.View(viewModel);
            }
            catch (Exception)
            {
                return this.RedirectToAction(GlobalConstants.ErrorAction, GlobalConstants.HomeController, new { Area = string.Empty });
            }
        }

        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                await this.ordersService.DeleteByIdAsync(id);
                return this.RedirectToAction(nameof(this.All), new { OrderStatus = this.TempData[GlobalConstants.ChangeStatusAction] });
            }
            catch (Exception)
            {
                return this.RedirectToAction(GlobalConstants.ErrorAction, GlobalConstants.HomeController, new { Area = string.Empty });
            }
        }

        public async Task<IActionResult> ChangeStatus(int id, OrderStatus orderStatus)
        {
            try
            {
                await this.ordersService.ChangeStatus(id, orderStatus);
                return this.RedirectToAction(nameof(this.All), new { OrderStatus = this.TempData[GlobalConstants.ChangeStatusAction] });
            }
            catch (Exception)
            {
                return this.RedirectToAction(GlobalConstants.ErrorAction, GlobalConstants.HomeController, new { Area = string.Empty });
            }
        }
    }
}
