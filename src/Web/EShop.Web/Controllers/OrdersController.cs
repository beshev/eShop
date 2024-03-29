﻿namespace EShop.Web.Controllers
{
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

            if (orderModel.OrderItems is null)
            {
                return this.BadRequest();
            }

            await this.ordersService.ComplateOrderAsync(orderModel);
            this.Session.Clear();

            return this.View(GlobalConstants.NameOfComplatedOrder, orderModel);
        }
    }
}
