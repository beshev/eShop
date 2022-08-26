namespace EShop.Services.Data.Orders
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
                    Bulstrad = model.UserInfo.Bulstrad,
                    DeliveryAddress = model.UserInfo.DeliveryAddress,
                    Mol = model.UserInfo.Mol,
                    Phone = model.UserInfo.Phone,
                },
            };

            foreach (var item in model.OrderItems)
            {
                var orderItem = new OrderInfo
                {
                    Price = item.Price,
                    Description = item.Description,
                    FontStyle = item.FontStyle.Value,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    TemplateId = item.TemplateId,
                    ProductId = item.ProductId,
                };

                foreach (var image in item.Images)
                {
                    orderItem.Images.Add(new Image { ImageUrl = await this.cloudinaryService.UploadAsync(image.Key, image.Value, "Orders") });
                }

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
