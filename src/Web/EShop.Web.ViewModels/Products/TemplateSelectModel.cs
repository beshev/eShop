namespace EShop.Web.ViewModels.Products
{
    using System.ComponentModel.DataAnnotations;

    using EShop.Common;

    public class TemplateSelectModel : SelectViewModel
    {
        [Display(Name = GlobalConstants.NameOfTemplates)]
        public override string Name { get => base.Name; set => base.Name = value; }
    }
}
