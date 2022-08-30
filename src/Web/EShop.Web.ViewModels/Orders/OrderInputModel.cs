namespace EShop.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    using EShop.Data.Models.Enums;
    using EShop.Web.ViewModels.ShoppingCarts;
    using EShop.Web.ViewModels.UserInfo;

    public class OrderInputModel
    {
        public OrderStatus Status { get; set; }

        public UserInfoInputModel UserInfo { get; set; }

        public IEnumerable<ShoppingCartModel> OrderItems { get; set; }
    }
}
