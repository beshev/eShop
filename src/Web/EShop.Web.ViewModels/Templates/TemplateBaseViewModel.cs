namespace EShop.Web.ViewModels.Templates
{
    using EShop.Data.Models;
    using EShop.Services.Mapping;

    public class TemplateBaseViewModel : IMapFrom<Template>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int ImagesCount { get; set; }

        public bool HasCustomText { get; set; }
    }
}
