namespace EShop.Services.Data.Orders
{
    using System;
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
                UserInfo = new UserInfo
                {
                    FirstName = model.UserInfo.FirstName,
                    LastName = model.UserInfo.LastName,
                    City = model.UserInfo.City,
                    Phone = model.UserInfo.Phone,
                    DeliveryAddress = model.UserInfo.DeliveryAddress,
                    DeliveryAddressType = model.UserInfo.DeliveryAddressType,
                    Carrier = model.UserInfo.Carrier,
                    Bullstat = model.UserInfo.Bullstat,
                    Mall = model.UserInfo.Mall,
                    CompanyAddress = model.UserInfo.CompanyAddress,
                    IsRegisteredByVAT = model.UserInfo.IsRegisteredByVAT,
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
                .Include(order => order.OrderItems)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            foreach (var orderItem in order.OrderItems)
            {
                var imagesUrls = orderItem.ImagesUrls.Split(GlobalConstants.Space, StringSplitOptions.RemoveEmptyEntries);
                foreach (var imageUrl in imagesUrls)
                {
                    var fileName = imageUrl
                        .Split(GlobalConstants.Slash, StringSplitOptions.RemoveEmptyEntries)
                        .Last()
                        .Split(GlobalConstants.Comma, StringSplitOptions.RemoveEmptyEntries)
                        .First();

                    await this.cloudinaryService.DeleteAsync(string.Format(GlobalConstants.OrdersCloundFolderName, fileName));
                }
            }

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

        public Task<int> GetCountAsync()
            => this.orderRepo
            .AllAsNoTracking()
            .CountAsync();
    }
}
