namespace EShop.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using EShop.Web.ViewModels.Products;
    using EShop.Web.ViewModels.Templates;

    public class IndexViewModel
    {
        public decimal TemplateCategoryPrice { get; set; }

        public string TemplateCategoryName { get; set; }

        public int TemplateCategoryId { get; set; }

        public IEnumerable<TemplateBaseViewModel> Templates { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
