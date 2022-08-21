namespace EShop.Web.ViewComponents
{
    using EShop.Common;
    using EShop.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Mvc;

    public class OrderInfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int? templateId, int? productId, decimal price, string productName, int imagesCount, bool hasCustomText)
        {
            this.TempData[GlobalConstants.NameOfOrderPrice] = price.ToString();
            this.TempData[GlobalConstants.NameOfOrderProductName] = productName;
            this.TempData[GlobalConstants.NameOfOrderTemplateId] = templateId;
            this.TempData[GlobalConstants.NameOfOrderProductId] = productId;

            var componentModel = new OrderInfoViewComponentModel
            {
                ImagesCount = imagesCount,
                HasCustomText = hasCustomText,
            };

            return this.View(componentModel);
        }
    }
}
