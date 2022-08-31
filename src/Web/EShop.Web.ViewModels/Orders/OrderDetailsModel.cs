namespace EShop.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;

    using EShop.Data.Models;
    using EShop.Services.Mapping;
    using EShop.Web.ViewModels.UserInfo;

    public class OrderDetailsModel : IMapFrom<Order>
    {
        // TODO: This can be self calculating
        public decimal TotalPrice { get; set; }

        public UserInfoInputModel UserInfo { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<OrderItemViewModel> OrderItems { get; set; }
    }
}
