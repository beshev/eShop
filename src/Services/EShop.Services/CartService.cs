namespace EShop.Services
{
    using System.IO;
    using System.Threading.Tasks;

    using EShop.Services.Mapping;
    using EShop.Web.ViewModels.Orders;
    using EShop.Web.ViewModels.ShoppingCarts;

    public class CartService : ICartService
    {
        public async Task<ShoppingCartModel> MapCartModelAsync(OrderItemInputModel model)
        {
            var cartItem = AutoMapperConfig.MapperInstance.Map<ShoppingCartModel>(model);

            if (model.Images is not null)
            {
                foreach (var image in model.Images)
                {
                    using var ms = new MemoryStream();
                    await image.CopyToAsync(ms);
                    cartItem.Images.Add(image.FileName, ms.ToArray());
                }
            }

            return cartItem;
        }
    }
}
