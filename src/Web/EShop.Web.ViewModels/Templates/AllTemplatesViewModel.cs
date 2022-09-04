namespace EShop.Web.ViewModels.Templates
{
    using System.Collections.Generic;

    public class AllTemplatesViewModel : PagingViewModel
    {
        public TemplateCategoryViewModel Category { get; set; }

        public int? SubCategoryId { get; set; }

        public IEnumerable<TemplateBaseViewModel> Templates { get; set; }
    }
}
