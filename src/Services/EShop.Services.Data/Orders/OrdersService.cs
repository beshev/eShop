﻿namespace EShop.Services.Data.Orders
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EShop.Common;
    using EShop.Data.Common.Repositories;
    using EShop.Data.Models;
    using EShop.Data.Models.Enums;
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

        public async Task ChangeStatus(int id, OrderStatus orderStatus)
        {
            var order = await this.orderRepo
                .All()
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
            order.Status = orderStatus;

            await this.orderRepo.SaveChangesAsync();
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

        public async Task DeleteByIdAsync(int id)
        {
            var order = await this.orderRepo
                .All()
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            this.orderRepo.Delete(order);
            await this.orderRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<TModel>> GetAllAsync<TModel>(OrderStatus orderStatus)
            => await this.orderRepo
            .AllAsNoTracking()
            .Where(x => x.Status.Equals(orderStatus))
            .To<TModel>()
            .ToListAsync();

        public async Task<TModel> GetByIdAsync<TModel>(int orderId)
            => await this.orderRepo
            .AllAsNoTracking()
            .Where(x => x.Id.Equals(orderId))
            .To<TModel>()
            .FirstOrDefaultAsync();
    }
}
