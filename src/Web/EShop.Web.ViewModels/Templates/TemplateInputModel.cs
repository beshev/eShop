namespace EShop.Web.ViewModels.Templates
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EShop.Common;
    using EShop.Web.Infrastructure.Attributes;
    using Microsoft.AspNetCore.Http;

    public class TemplateInputModel
    {
        [Required(ErrorMessage = ErrorMessagesConstants.RequiredField)]
        [MaxLength(DataConstants.TemplateNameMaxLength, ErrorMessage = ErrorMessagesConstants.TemplateNameLengthErrorMessage)]
        [Display(Name = GlobalConstants.TemplateDisplayName)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessagesConstants.RequiredField)]
        [Display(Name = GlobalConstants.TemplateDisplayDescription)]
        public string Description { get; set; }

        [Required(ErrorMessage = ErrorMessagesConstants.RequiredField)]
        [Display(Name = GlobalConstants.TemplateDisplayPrice)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = ErrorMessagesConstants.RequiredField)]
        [Display(Name = GlobalConstants.TemplateDisplayImage)]
        [AllowedExtensionsAttribute]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = ErrorMessagesConstants.RequiredField)]
        [Display(Name = GlobalConstants.DisplayImagesCount)]
        public int ImagesFixedCount { get; set; }

        [Display(Name = GlobalConstants.DisplayHasCustomText)]
        public bool HasCustomText { get; set; }

        [Display(Name = GlobalConstants.DisplayIsBaseModel)]
        public bool IsBaseModel { get; set; }

        public int? SubCategoryId { get; set; }

        [Required(ErrorMessage = ErrorMessagesConstants.RequiredField)]
        public IEnumerable<int> CategoriesIds { get; set; }
    }
}
