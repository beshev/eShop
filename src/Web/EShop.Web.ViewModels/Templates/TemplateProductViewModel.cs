namespace EShop.Web.ViewModels.Products
{
    using EShop.Data.Models;
    using EShop.Services.Mapping;

    public class TemplateProductViewModel : SelectViewModel, IMapFrom<Product>
    {
        public string ImageUrl { get; set; }

        public decimal Price { get; set; }
    }
}
