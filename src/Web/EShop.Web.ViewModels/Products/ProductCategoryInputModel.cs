namespace EShop.Web.ViewModels.Products
{
    using System.ComponentModel.DataAnnotations;

    using EShop.Common;

    public class ProductCategoryInputModel
    {
        [Required]
        [MaxLength(DataConstants.ProductCategoryNameMaxLength)]
        public string Name { get; set; }
    }
}
