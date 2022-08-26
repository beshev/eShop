namespace EShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EShop.Data.Common.Models;

    // TODO: Rename this class to OrderItem
    public class OrderInfo : BaseModel<int>
    {
        public OrderInfo()
        {
            this.Images = new HashSet<Image>();
        }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public int? FontStyle { get; set; }

        [Required]
        [MaxLength(255)]
        public string ProductName { get; set; }

        public int? TemplateId { get; set; }

        public virtual Template Template { get; set; }

        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
