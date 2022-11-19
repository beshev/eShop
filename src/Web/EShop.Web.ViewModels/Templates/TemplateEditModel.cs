namespace EShop.Web.ViewModels.Templates
{
    using EShop.Data.Models;
    using EShop.Services.Mapping;

    public class TemplateEditModel : TemplateInputModel, IMapFrom<Template>
    {
        public int Id { get; set; }
    }
}
