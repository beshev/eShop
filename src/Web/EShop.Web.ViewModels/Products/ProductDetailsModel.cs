namespace EShop.Web.ViewModels.Products
{
    using EShop.Data.Models;
    using EShop.Services.Mapping;

    public class ProductDetailsModel : ProductViewModel, IMapFrom<Product>
    {
        public bool HasCustomText { get; set; }

        public bool HasFontStyle { get; set; }

        public int ImagesCount { get; set; }
    }
}
