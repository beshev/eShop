namespace EShop.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EShop.Common;
    using EShop.Services;
    using EShop.Web.Infrastructure.Extensions;
    using EShop.Web.ViewModels.Orders;
    using EShop.Web.ViewModels.ShoppingCarts;
    using Microsoft.AspNetCore.Mvc;

    public class CartController : BaseController
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public IActionResult Items()
        {
            // TODO: Put if statment in VIEW
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
            return this.RedirectToAction(GlobalConstants.AllAction, GlobalConstants.TemplatesController, new { model.ProductId });
        }

        public IActionResult RemoveItem(string id)
        {
            var cartItem = this.Session.GetCollection<ShoppingCartModel>(GlobalConstants.NameOfCart);
            cartItem = cartItem.Where(x => x.Id.Equals(id) == false).ToList();
            this.Session.SetCollection<ShoppingCartModel>(GlobalConstants.NameOfCart, cartItem);

            // TODO: Use constnas
            return this.RedirectToAction("Items", "Cart");
        }
    }
}
