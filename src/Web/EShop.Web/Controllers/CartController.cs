namespace EShop.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EShop.Common;
    using EShop.Services;
    using Eshop.Services.Cloudinary;
    using EShop.Web.Infrastructure.Extensions;
    using EShop.Web.ViewModels.Orders;
    using EShop.Web.ViewModels.ShoppingCarts;
    using Microsoft.AspNetCore.Mvc;

    public class CartController : BaseController
    {
        private readonly ICloudinaryService cloudinaryService;
        private readonly ICartService cartService;

        public CartController(
            ICloudinaryService cloudinaryService,
            ICartService cartService)
        {
            this.cloudinaryService = cloudinaryService;
            this.cartService = cartService;
        }

        public IActionResult Items()
        {
            var viewModel = this.Session.GetCollection<ShoppingCartModel>(GlobalConstants.NameOfCart) ?? new List<ShoppingCartModel>();
            return this.View(viewModel);
        }

        public async Task<IActionResult> AddItem(OrderInfoInputModel model)
        {
            model.ProductName = this.TempData[GlobalConstants.NameOfOrderProductName] as string;
            model.Price = decimal.Parse(this.TempData[GlobalConstants.NameOfOrderPrice] as string);
            model.TemplateId = this.TempData[GlobalConstants.NameOfOrderTemplateId] as int?;
            model.ProductId = this.TempData[GlobalConstants.NameOfOrderProductId] as int?;

            if (model.TemplateId is null || model.ProductId is null)
            {
                return this.NotFound();
            }

            var cartItems = this.Session.GetCollection<ShoppingCartModel>(GlobalConstants.NameOfCart) ?? new List<ShoppingCartModel>();
            var cartItem = await this.cartService.MapCartModelAsync(model);
            cartItems.Add(cartItem);

            this.Session.SetCollection<ShoppingCartModel>(GlobalConstants.NameOfCart, cartItems);
            return this.RedirectToAction(GlobalConstants.All, GlobalConstants.Template, new { model.ProductId });
        }

        public async Task<IActionResult> ComplateOrder()
        {
            var cart = this.Session.GetCollection<ShoppingCartModel>(GlobalConstants.NameOfCart);

            foreach (var item in cart)
            {
                foreach (var image in item.Images)
                {
                    await this.cloudinaryService.UploadAsync(image.Key, image.Value, $"cartItems/{image.Key}");
                }
            }

            return this.RedirectToAction(GlobalConstants.All, GlobalConstants.Template, new { ProductId = 5 });
        }
    }
}
