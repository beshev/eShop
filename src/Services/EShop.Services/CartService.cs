namespace EShop.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using EShop.Common;
    using EShop.Services.Mapping;
    using EShop.Web.ViewModels.Orders;
    using EShop.Web.ViewModels.ShoppingCarts;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;

    public class CartService : ICartService
    {
        public async Task<ShoppingCartModel> MapCartModelAsync(OrderInfoInputModel model)
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
