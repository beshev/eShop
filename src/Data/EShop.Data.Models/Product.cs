﻿namespace EShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EShop.Common;
    using EShop.Data.Common.Models;

    public class Product : BaseModel<int>
    {
        public Product()
        {
            this.ProductOrders = new HashSet<OrderItem>();
        }

        [Required]
        [MaxLength(DataConstants.ProductNameMaxLength)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public bool HasCustomText { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int ProductCategoryId { get; set; }

        public ProductCategory ProductCategory { get; set; }

        public virtual IEnumerable<OrderItem> ProductOrders { get; set; }
    }
}
