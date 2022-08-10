namespace EShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EShop.Data.Common.Models;

    public class Product : BaseModel<int>
    {
        public Product()
        {
            this.ProductTemplates = new HashSet<ProductTemplate>();
            this.ProductOrders = new HashSet<OrderInfo>();
            this.Images = new HashSet<Image>();
        }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public virtual IEnumerable<Image> Images { get; set; }

        public virtual IEnumerable<ProductTemplate> ProductTemplates { get; set; }

        public virtual IEnumerable<OrderInfo> ProductOrders { get; set; }
    }
}
