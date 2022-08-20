namespace EShop.Web.ViewModels.Templates
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class TemplateInputModel
    {
        [Required]
        [MaxLength(255)]
        [Display(Name = "Име на темплейта")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public decimal Price { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        public int ImagesFixedCount { get; set; }

        public bool HasCustomText { get; set; }

        public bool IsBaseModel { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<int> ProductsIds { get; set; }
    }
}
