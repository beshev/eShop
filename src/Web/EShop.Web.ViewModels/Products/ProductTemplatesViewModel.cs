namespace EShop.Web.ViewModels.Products
{
    using System.Collections.Generic;

    using EShop.Web.ViewModels.Templates;

    public class ProductTemplatesViewModel
    {
        public ProductSelectModel Product { get; set; }

        public IEnumerable<TemplateViewModel> Templates { get; set; }
    }
}
