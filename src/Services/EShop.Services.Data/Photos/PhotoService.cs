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
        private readonly ICloudinaryService cloudinaryService;

        public PhotoService(
            IRepository<Photo> photoRepo,
            ICloudinaryService cloudinaryService)
        {
            this.photoRepo = photoRepo;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task AddPhotoAsync(IFormFile photo)
        {
            var image = new Photo
            {
                Name = photo.FileName,
                ImageUrl = await this.cloudinaryService.UploadAsync(photo, string.Format(GlobalConstants.GalleryCloudFolder, photo.FileName)),
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

            await this.cloudinaryService.DeleteAsync(string.Format(GlobalConstants.GalleryCloudFolder, photo.Name));
            this.photoRepo.Delete(photo);
            await this.photoRepo.SaveChangesAsync();
        }
    }
}
