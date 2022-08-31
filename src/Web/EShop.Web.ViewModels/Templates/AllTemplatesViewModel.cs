namespace EShop.Web.ViewModels.Templates
{
    using System.Collections.Generic;

    using EShop.Web.ViewModels.Products;

    public class AllTemplatesViewModel : PagingViewModel
    {
        public TemplateProductViewModel Product { get; set; }

        public IEnumerable<TemplateBaseViewModel> Templates { get; set; }
    }
}
