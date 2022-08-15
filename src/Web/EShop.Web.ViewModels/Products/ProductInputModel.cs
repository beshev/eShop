namespace EShop.Web.ViewModels.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EShop.Common;
    using Microsoft.AspNetCore.Http;

    public class ProductInputModel
    {
        [Required(ErrorMessage = ErrorMessagesConstants.RequiredField)]
        [MaxLength(DataConstants.ProductNameMaxLength)]
        [Display(Name = GlobalConstants.NameOfName)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessagesConstants.RequiredField)]
        [Display(Name = GlobalConstants.NameOfPrice)]
        public decimal Price { get; set; }

        [Display(Name = GlobalConstants.NameOfDescription)]
        public string Description { get; set; }

        [Display(Name = GlobalConstants.NameOfHasCustomText)]
        public bool HasCustomText { get; set; }

        [Required(ErrorMessage = ErrorMessagesConstants.RequiredField)]
        [Display(Name = GlobalConstants.NameOfImage)]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = ErrorMessagesConstants.RequiredField)]
        public int CategoryId { get; set; }

        public IEnumerable<int> TemplatesIds { get; set; }
    }
}
