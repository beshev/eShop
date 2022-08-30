namespace EShop.Services.Data.Orders
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EShop.Common;
    using EShop.Data.Common.Repositories;
    using EShop.Data.Models;
    using Eshop.Services.Cloudinary;
    using EShop.Services.Mapping;
    using EShop.Web.ViewModels.Orders;
    using Microsoft.EntityFrameworkCore;

    public class OrdersService : IOrdersService
    {
        private readonly IRepository<Order> orderRepo;
        private readonly ICloudinaryService cloudinaryService;

        public OrdersService(
            IRepository<Order> orderRepo,
            ICloudinaryService cloudinaryService)
        {
            this.orderRepo = orderRepo;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task ComplateOrderAsync(OrderInputModel model)
        {
            var order = new Order
            {
                TotalPrice = model.OrderItems.Sum(i => i.Price),
                UserInfo = new UserInfo
                {
                    FirsName = model.UserInfo.FirsName,
                    LastName = model.UserInfo.LastName,
                    City = model.UserInfo.City,
                    Bullstat = model.UserInfo.Bullstat,
                    DeliveryAddress = model.UserInfo.DeliveryAddress,
                    Mall = model.UserInfo.Mall,
                    Phone = model.UserInfo.Phone,
                },
            };

            foreach (var item in model.OrderItems)
            {
                var orderItem = new OrderItem
                {
                    Price = item.Price,
                    Description = item.Description,
                    FontStyle = item.FontStyle ?? null,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    TemplateId = item.TemplateId,
                    ProductId = item.ProductId,
                };

                var imageUrls = new StringBuilder();
                foreach (var image in item.Images)
                {
                    imageUrls.Append(await this.cloudinaryService.UploadAsync(image.Key, image.Value, string.Format(GlobalConstants.OrdersCloundFolderName, image.Key)));
                    imageUrls.Append(GlobalConstants.Space);
                }

                orderItem.ImagesUrls = imageUrls.ToString();
                order.OrderItems.Add(orderItem);
            }

            await this.orderRepo.AddAsync(order);
            await this.orderRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<TModel>> GetAllAsync<TModel>()
            => await this.orderRepo
            .AllAsNoTracking()
            .To<TModel>()
            .ToListAsync();
    }
}
