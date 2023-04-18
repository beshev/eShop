namespace Eshop.Services.Cloudinary
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IImagesService
    {
        public Task<string> UploadAsync(IFormFile file, string folder);

        public Task<string> UploadAsync(string fileName, byte[] fileBytes, string folder);

        public void Delete(string imagePath, string folder);
    }
}
