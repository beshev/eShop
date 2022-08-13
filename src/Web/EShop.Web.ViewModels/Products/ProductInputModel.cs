namespace EShop.Web.ViewModels.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EShop.Common;
    using Microsoft.AspNetCore.Http;

    public class ProductInputModel
    {
        [Required]
        [MaxLength(DataConstants.ProductNameMaxLength)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public bool HasCustomText { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        public int ProductCategoryId { get; set; }

        public IEnumerable<int> TemplatesIds { get; set; }
    }
}
