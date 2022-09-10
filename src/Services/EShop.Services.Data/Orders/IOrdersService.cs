namespace EShop.Services.Data.Orders
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EShop.Data.Models.Enums;
    using EShop.Web.ViewModels.Orders;

    public interface IOrdersService
    {
        public Task ComplateOrderAsync(OrderInputModel order);

        public Task<IEnumerable<TModel>> GetAllAsync<TModel>(OrderStatus orderStatus);

        public Task<TModel> GetByIdAsync<TModel>(int orderId);

        public Task DeleteByIdAsync(int id);

        public Task<int> GetCountAsync();

        public Task ChangeStatus(int id, OrderStatus orderStatus);
    }
}
