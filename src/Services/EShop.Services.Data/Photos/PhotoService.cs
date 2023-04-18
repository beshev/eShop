namespace EShop.Services.Data.Photos
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EShop.Common;
    using EShop.Data.Common.Repositories;
    using EShop.Data.Models;
    using Eshop.Services.Cloudinary;
    using EShop.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class PhotoService : IPhotoService
    {
        private readonly IRepository<Photo> photoRepo;
        private readonly IImagesService imagesService;

        public PhotoService(
            IRepository<Photo> photoRepo,
            IImagesService imagesService)
        {
            this.photoRepo = photoRepo;
            this.imagesService = imagesService;
        }

        public async Task AddPhotoAsync(IFormFile photo)
        {
            var image = new Photo
            {
                Name = photo.FileName,
                ImageUrl = await this.imagesService.UploadAsync(photo, GlobalConstants.GalleryFolderName),
            };

            await this.photoRepo.AddAsync(image);
            await this.photoRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<TModel>> AllAsync<TModel>()
            => await this.photoRepo
            .AllAsNoTracking()
            .OrderByDescending(x => x.CreatedOn)
            .To<TModel>()
            .ToListAsync();

        public async Task DeletePhotoAsync(int id)
        {
            var photo = await this.photoRepo
                .All()
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            this.imagesService.Delete(photo.ImageUrl, GlobalConstants.GalleryFolderName);
            this.photoRepo.Delete(photo);
            await this.photoRepo.SaveChangesAsync();
        }
    }
}
