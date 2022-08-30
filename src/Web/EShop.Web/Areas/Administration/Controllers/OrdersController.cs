﻿namespace EShop.Web.Areas.Administration.Controllers
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
    }
}