namespace EShop.Web.ViewModels.Orders
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EShop.Common;
    using Microsoft.AspNetCore.Http;

    public class OrderItemInputModel
    {
        public decimal Price { get; set; }

        [Display(Name = ErrorMessagesConstants.DisplayItemDescription)]
        public string Description { get; set; }

        [Required(ErrorMessage = ErrorMessagesConstants.QuantityField)]
        [Display(Name = GlobalConstants.NameOfQuantity)]
        public int Quantity { get; set; }

        [Required(ErrorMessage = ErrorMessagesConstants.DisplayItemFontStyle)]
        [Display(Name = GlobalConstants.NameOfFontStyle)]
        public int? FontStyle { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }

        [MaxLength(DataConstants.ProductNameMaxLength)]
        public string ProductName { get; set; }

        public int? TemplateId { get; set; }

        public int? ProductId { get; set; }
    }
}
