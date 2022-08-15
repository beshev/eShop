namespace EShop.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using EShop.Common;
    using EShop.Data.Models;
    using EShop.Services.Mapping;

    public class SelectViewModel :
        IMapFrom<ProductCategory>,
        IMapFrom<TemplateCategory>,
        IMapFrom<Template>
    {
        public int Id { get; set; }

        [Display(Name = GlobalConstants.NameOfCategory)]
        public virtual string Name { get; set; }
    }
}
