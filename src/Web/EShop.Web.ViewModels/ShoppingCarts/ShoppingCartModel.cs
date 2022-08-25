namespace EShop.Web.ViewModels.ShoppingCarts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using EShop.Common;
    using EShop.Services.Mapping;
    using EShop.Web.ViewModels.Orders;

    public class ShoppingCartModel : IMapFrom<OrderInfoInputModel>, IHaveCustomMappings
    {
        public ShoppingCartModel()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Display(Name = GlobalConstants.NameOfPrice)]
        public decimal Price { get; set; }

        [Display(Name = GlobalConstants.NameOfDescription)]
        public string Description { get; set; }

        [Display(Name = GlobalConstants.NameOfQuantity)]
        public int Quantity { get; set; }

        [Display(Name = GlobalConstants.NameOfFontStyle)]
        public int? FontStyle { get; set; }

        public virtual Dictionary<string, byte[]> Images { get; set; } = new Dictionary<string, byte[]>();

        [Display(Name = GlobalConstants.NameOfProducts)]
        public string ProductName { get; set; }

        public int? TemplateId { get; set; }

        public int? ProductId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<OrderInfoInputModel, ShoppingCartModel>()
                .ForMember(ders => ders.Images, src => src.Ignore());
        }
    }
}
