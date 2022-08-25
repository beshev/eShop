namespace EShop.Web.ViewModels.ShoppingCarts
{
    using System.Collections.Generic;

    using AutoMapper;
    using EShop.Services.Mapping;
    using EShop.Web.ViewModels.Orders;

    public class ShoppingCartModel : IMapFrom<OrderInfoInputModel>, IHaveCustomMappings
    {
        public decimal Price { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public int FontStyle { get; set; }

        public virtual Dictionary<string, byte[]> Images { get; set; }

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
