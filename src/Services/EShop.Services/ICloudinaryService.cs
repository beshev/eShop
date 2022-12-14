namespace Eshop.Services.Cloudinary
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        public Task<string> UploadAsync(IFormFile file, string fileName);

        public Task<string> UploadAsync(string fileName, byte[] bytes, string cloudFolder);

        public Task DeleteAsync(string cloudFolder);

        public Task<string> RenameAsync(string oldCloudPath, string newCloudPath);
    }
}
