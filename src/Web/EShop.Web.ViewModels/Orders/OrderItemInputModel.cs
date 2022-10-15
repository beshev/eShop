namespace EShop.Web.ViewModels.Orders
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EShop.Common;
    using EShop.Web.Infrastructure.Attributes;
    using Microsoft.AspNetCore.Http;

    public class OrderItemInputModel
    {
        public decimal Price { get; set; }

        [Display(Name = ErrorMessagesConstants.DisplayItemDescription)]
        public string Description { get; set; }

        [Display(Name = ErrorMessagesConstants.DisplayItemCustomerNote)]
        public string CustomerNote { get; set; }

        [Display(Name = GlobalConstants.NameOfQuantity)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessagesConstants.QuantityField)]
        public int Quantity { get; set; }

        [Display(Name = GlobalConstants.NameOfFontStyle)]
        public int? FontStyle { get; set; }

        [AllowedExtensions]
        public IEnumerable<IFormFile> Images { get; set; }

        [MaxLength(DataConstants.ProductNameMaxLength)]
        public string ProductName { get; set; }

        public string ProductImageUrl { get; set; }

        public int? TemplateId { get; set; }

        public int? ProductId { get; set; }
    }
}
