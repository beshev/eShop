namespace EShop.Web.ViewModels.Gallery
{
    using EShop.Data.Models;
    using EShop.Services.Mapping;

    public class PhotoViewModel : IMapFrom<Photo>
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }
    }
}
