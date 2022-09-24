namespace EShop.Services.Data.Photos
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IPhotoService
    {
        public Task<IEnumerable<TModel>> AllAsync<TModel>();

        public Task AddPhotoAsync(IFormFile photo);

        public Task DeletePhotoAsync(int id);
    }
}
