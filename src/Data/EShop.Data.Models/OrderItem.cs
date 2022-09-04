namespace EShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using EShop.Common;
    using EShop.Data.Common.Models;

    public class OrderItem : BaseModel<int>
    {
        public decimal Price { get; set; }

        public string ImagesUrls { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public int? FontStyle { get; set; }

        [Required]
        [MaxLength(DataConstants.ProductNameMaxLength)]

        public string ProductName { get; set; }

        public int? TemplateId { get; set; }

        public virtual Template Template { get; set; }

        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
