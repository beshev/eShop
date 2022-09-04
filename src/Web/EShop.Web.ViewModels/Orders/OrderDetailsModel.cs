namespace EShop.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EShop.Data.Models;
    using EShop.Services.Mapping;
    using EShop.Web.ViewModels.UserInfo;

    public class OrderDetailsModel : IMapFrom<Order>
    {
        public decimal TotalPrice => this.OrderItems.Sum(x => x.Price * x.Quantity);

        public UserInfoInputModel UserInfo { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<OrderItemViewModel> OrderItems { get; set; }
    }
}
