﻿namespace EShop.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EShop.Common;
    using EShop.Data.Models.Enums;
    using EShop.Services.Data.Orders;
    using EShop.Web.Infrastructure.Extensions;
    using EShop.Web.ViewModels.Orders;
    using EShop.Web.ViewModels.ShoppingCarts;
    using EShop.Web.ViewModels.UserInfo;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : BaseController
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        public IActionResult ComplateOrder()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ComplateOrder(UserInfoInputModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            var orderModel = new OrderInputModel
            {
                UserInfo = model,
                Status = OrderStatus.Active,
                OrderItems = this.Session.GetCollection<ShoppingCartModel>(GlobalConstants.NameOfCart),
            };

            // Change order Id to be GUID !!
            await this.ordersService.ComplateOrderAsync(orderModel);
            this.Session.Clear();

            // Send order Id and remove this
            this.TempData.Put(GlobalConstants.ComplatedOrder, orderModel);

            return this.RedirectToAction(nameof(this.ComplatedOrder));
        }

        public IActionResult ComplatedOrder()
        {
            // TODO: User another view model
            // TODO: Take the order details from the database via order id passed to action
            var viewModel = this.TempData.Get<OrderInputModel>(GlobalConstants.ComplatedOrder);
            this.TempData.Put(GlobalConstants.ComplatedOrder, viewModel);
            if (viewModel is null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
