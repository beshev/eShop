namespace Eshop.Services.Cloudinary
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using EShop.Common;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

    public class ImagesService : IImagesService
    {
        private readonly IWebHostEnvironment environment;

        public ImagesService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<string> UploadAsync(IFormFile file, string folder)
        {
            if (file == null)
            {
                return string.Empty;
            }

            byte[] actualFile;
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                actualFile = ms.ToArray();
            }

            return await this.UploadAsync(file.FileName, actualFile, folder);
        }

        public async Task<string> UploadAsync(string fileName, byte[] fileBytes, string folder)
        {
            var imageFullFolderPath = Path.Combine(this.environment.WebRootPath, GlobalConstants.ImagesFolderName, folder, fileName);
            await File.WriteAllBytesAsync(imageFullFolderPath, fileBytes);

            var imagePath = Path.Combine(@$"\{GlobalConstants.ImagesFolderName}", folder, fileName);
            return imagePath;
        }

        public void Delete(string imagePath, string folder)
        {
            if (string.IsNullOrWhiteSpace(imagePath)
                || string.IsNullOrWhiteSpace(folder))
            {
                return;
            }

            var imageName = imagePath
                    .Split('\\', StringSplitOptions.RemoveEmptyEntries)
                    .Last();
            var filePath = Path.Combine(this.environment.WebRootPath, GlobalConstants.ImagesFolderName, folder, imageName);
            File.Delete(filePath);
        }
    }
}
