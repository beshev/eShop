namespace EShop.Web.ViewModels.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EShop.Common;
    using EShop.Web.Infrastructure.Attributes;
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

        [Display(Name = GlobalConstants.DisplayHasFontStyle)]
        public bool HasFontStyle { get; set; }

        [Display(Name = GlobalConstants.DisplayImagesCount)]
        public int ImagesCount { get; set; }

        [Required(ErrorMessage = ErrorMessagesConstants.RequiredField)]
        [Display(Name = GlobalConstants.NameOfImage)]
        [AllowedExtensionsAttribute]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = ErrorMessagesConstants.RequiredField)]
        public int CategoryId { get; set; }
    }
}
