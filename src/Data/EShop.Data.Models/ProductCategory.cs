namespace EShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EShop.Data.Common.Models;

    public class ProductCategory : BaseModel<int>
    {
        public ProductCategory()
        {
            this.Products = new HashSet<Product>();
        }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
