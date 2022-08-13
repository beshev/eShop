namespace EShop.Web.ViewModels.Products
{
    using System.ComponentModel.DataAnnotations;

    using EShop.Common;
    using Microsoft.AspNetCore.Http;

    public class ProductInputModel
    {
        [Required]
        [MaxLength(DataConstants.ProductNameMaxLength)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        public int ProductCategoryId { get; set; }
    }
}
