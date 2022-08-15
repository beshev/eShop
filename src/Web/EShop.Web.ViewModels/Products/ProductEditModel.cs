namespace EShop.Web.ViewModels.Products
{
    using EShop.Data.Models;
    using EShop.Services.Mapping;

    public class ProductEditModel : ProductInputModel, IMapFrom<Product>
    {
        public int Id { get; set; }
    }
}
