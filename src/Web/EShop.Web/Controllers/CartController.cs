namespace EShop.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EShop.Common;
    using EShop.Services;
    using EShop.Web.Infrastructure.Attributes;
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
            var viewModel = this.Session.GetCollection<ShoppingCartModel>(GlobalConstants.NameOfCart) ?? Enumerable.Empty<ShoppingCartModel>();
            return this.View(viewModel);
        }

        [HttpPost]
        [SetTempDataErrors(GlobalConstants.ModelStateErrorsKey)]
        public async Task<IActionResult> AddItem(OrderItemInputModel model, string returnUrl)
        {
            model.ProductName = this.TempData[GlobalConstants.NameOfOrderProductName] as string;
            model.Price = decimal.Parse(this.TempData[GlobalConstants.NameOfOrderPrice] as string);
            model.TemplateId = this.TempData[GlobalConstants.NameOfOrderTemplateId] as int?;
            model.ProductId = this.TempData[GlobalConstants.NameOfOrderProductId] as int?;

            if (this.ModelState.IsValid)
            {
                var cartItems = this.Session.GetCollection<ShoppingCartModel>(GlobalConstants.NameOfCart) ?? new List<ShoppingCartModel>();
                var cartItem = await this.cartService.MapCartModelAsync(model);
                cartItems.Add(cartItem);

                this.Session.SetCollection<ShoppingCartModel>(GlobalConstants.NameOfCart, cartItems);
                this.TempData[GlobalConstants.SuccessKey] = true;
            }

            return this.Redirect(returnUrl);
        }

        public IActionResult RemoveItem(string id)
        {
            var cartItems = this.Session.GetCollection<ShoppingCartModel>(GlobalConstants.NameOfCart);
            cartItems = cartItems.Where(x => x.Id.Equals(id) == false).ToList();
            this.Session.SetCollection<ShoppingCartModel>(GlobalConstants.NameOfCart, cartItems);

            // TODO: Use constnas
            return this.RedirectToAction("Items", "Cart");
        }
    }
}
