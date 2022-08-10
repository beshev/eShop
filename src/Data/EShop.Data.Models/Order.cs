﻿namespace EShop.Data.Models
{
    using System.Collections.Generic;

    using EShop.Data.Common.Models;
    using EShop.Data.Models.Enums;

    public class Order : BaseModel<int>
    {
        public Order()
        {
            this.TemplatesOrders = new HashSet<OrderInfo>();
        }

        public decimal TotalPrice { get; set; }

        public OrderStatus Status { get; set; }

        public virtual UserInfo UserInfo { get; set; }

        public virtual IEnumerable<OrderInfo> TemplatesOrders { get; set; }
    }
}