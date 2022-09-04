namespace EShop.Web.ViewModels.Templates
{
    using EShop.Data.Models;
    using EShop.Services.Mapping;

    public class TemplateCategoryViewModel : IMapFrom<TemplateCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
