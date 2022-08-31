namespace EShop.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    using EShop.Data.Models.Enums;

    public class AllOrdersViewModel
    {
        public IEnumerable<OrderViewModel> Orders { get; set; }

        public OrderStatus Status { get; set; }
    }
}
