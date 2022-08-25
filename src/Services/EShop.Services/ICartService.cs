namespace EShop.Services
{
    using System.Threading.Tasks;

    using EShop.Web.ViewModels.Orders;
    using EShop.Web.ViewModels.ShoppingCarts;

    public interface ICartService
    {
        public Task<ShoppingCartModel> MapCartModelAsync(OrderInfoInputModel orderInfoInputModel);
    }
}
