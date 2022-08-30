namespace EShop.Web.ViewModels.Orders
{
    using System;

    using EShop.Data.Models;
    using EShop.Services.Mapping;
    using EShop.Web.ViewModels.UserInfo;

    public class OrderViewModel : IMapFrom<Order>
    {
        public decimal TotalPrice { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserInfoViewModel UserInfo { get; set; }
    }
}
