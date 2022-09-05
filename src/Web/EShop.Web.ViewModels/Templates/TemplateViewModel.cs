namespace EShop.Web.ViewModels.Templates
{
    public class TemplateViewModel : TemplateBaseViewModel
    {
        public int ImagesCount { get; set; }

        public bool HasCustomText { get; set; }

        public bool IsBaseModel { get; set; }

        public int CategoryId { get; set; }
    }
}
