namespace EShop.Web.Controllers
{
    using System.Threading.Tasks;

    using EShop.Services.Data.Photos;
    using EShop.Web.ViewModels.Gallery;
    using Microsoft.AspNetCore.Mvc;

    public class GalleryController : BaseController
    {
        private readonly IPhotoService photoService;

        public GalleryController(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        public async Task<IActionResult> Gallery()
        {
            var viewModel = await this.photoService.AllAsync<PhotoViewModel>();
            return this.View(viewModel);
        }
    }
}
