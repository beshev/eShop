namespace EShop.Web.Areas.Administration.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using EShop.Common;
    using EShop.Services.Data.Photos;
    using EShop.Web.Infrastructure.Attributes;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class GalleryController : AdministrationController
    {
        private readonly IPhotoService photoService;

        public GalleryController(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        [HttpPost]
        [SetTempDataErrorsAttribute(GlobalConstants.NameOfGallery)]
        public async Task<IActionResult> AddPhoto([AllowedExtensions, Required(ErrorMessage = ErrorMessagesConstants.RequiredField)] IFormFile photo)
        {
            if (this.ModelState.IsValid)
            {
                await this.photoService.AddPhotoAsync(photo);
            }

            return this.RedirectToAction(GlobalConstants.NameOfGallery, new { Area = string.Empty });
        }

        [HttpPost]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            await this.photoService.DeletePhotoAsync(id);
            return this.RedirectToAction(GlobalConstants.NameOfGallery, new { Area = string.Empty });
        }
    }
}
