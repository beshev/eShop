namespace EShop.Web.ViewModels.Products
{
    using System.Collections.Generic;

    public class AllProductsViewModel : PagingViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
