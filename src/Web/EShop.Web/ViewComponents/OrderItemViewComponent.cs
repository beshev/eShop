﻿namespace EShop.Web.ViewComponents
{
    using EShop.Common;
    using EShop.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Mvc;

    public class OrderItemViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(
            int? templateId,
            int? productId,
            decimal price,
            string productName,
            int imagesCount,
            bool hasCustomText,
            string productImageUrl,
            string returnUrl,
            bool hasFontStyle = true)
        {
            this.TempData[GlobalConstants.NameOfOrderPrice] = price.ToString();
            this.TempData[GlobalConstants.NameOfOrderProductName] = productName;
            this.TempData[GlobalConstants.NameOfOrderTemplateId] = templateId;
            this.TempData[GlobalConstants.NameOfOrderProductId] = productId;

            var componentModel = new OrderItemViewComponentModel
            {
                ImagesCount = imagesCount,
                ReturnUrl = returnUrl,
                HasCustomText = hasCustomText,
                HasFontStyle = hasFontStyle,
                OrderInfo = new OrderItemInputModel { ProductImageUrl = productImageUrl },
            };

            return this.View(componentModel);
        }
    }
}
