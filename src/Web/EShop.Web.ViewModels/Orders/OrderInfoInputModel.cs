namespace EShop.Web.ViewModels.Orders
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class OrderInfoInputModel
    {
        public decimal Price { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        [Range(1, 7)]
        public int FontStyle { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }

        [Required]
        [MaxLength(255)]
        public string ProductName { get; set; }

        public int? TemplateId { get; set; }

        public int? ProductId { get; set; }
    }
}
