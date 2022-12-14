namespace Eshop.Services.Cloudinary
{
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> UploadAsync(IFormFile file, string cloudFolder)
        {
            if (file == null)
            {
                return string.Empty;
            }

            byte[] destination;

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                destination = ms.ToArray();
            }

            return await this.UploadAsync(file.FileName, destination, cloudFolder);
        }

        public async Task<string> UploadAsync(string fileName, byte[] bytes, string cloudFolder)
        {
            UploadResult uploadResult;

            await using (var ms = new MemoryStream(bytes))
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(fileName, ms),
                    PublicId = $"E-shop/{cloudFolder}",
                    Overwrite = true,
                };

                uploadResult = await this.cloudinary.UploadAsync(uploadParams);
            }

            var secureUrl = uploadResult.Url;

            return secureUrl.OriginalString;
        }

        public async Task DeleteAsync(string cloudFolder)
         => await this.cloudinary.DestroyAsync(new DeletionParams($"E-shop/{cloudFolder}"));

        public async Task<string> RenameAsync(string oldCloudPath, string newCloudPath)
        {
            var result = await this.cloudinary.RenameAsync($"E-shop/{oldCloudPath}", $"E-shop/{newCloudPath}");
            return result.SecureUrl;
        }
    }
}
