namespace EShop.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using EShop.Web.ViewModels.Templates;

    public class IndexViewModel
    {
        public int TemplateCategoryId { get; set; }

        public IEnumerable<TemplateBaseViewModel> Templates { get; set; }
    }
}
