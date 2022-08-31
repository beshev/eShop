namespace EShop.Services.Data.Orders
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EShop.Web.ViewModels.Orders;

    public interface IOrdersService
    {
        public Task ComplateOrderAsync(OrderInputModel order);

        public Task<IEnumerable<TModel>> GetAllAsync<TModel>();

        public Task<TModel> GetByIdAsync<TModel>(int orderId);

        public Task DeleteByIdAsync(int id);
    }
}
