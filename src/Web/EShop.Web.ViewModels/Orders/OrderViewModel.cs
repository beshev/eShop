namespace EShop.Web.ViewModels.Orders
{
    using System;
    using System.Linq;

    using AutoMapper;
    using EShop.Data.Models;
    using EShop.Services.Mapping;
    using EShop.Web.ViewModels.UserInfo;

    public class OrderViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserInfoViewModel UserInfo { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Order, OrderViewModel>()
                .ForMember(x => x.TotalPrice, src => src.MapFrom(x => x.OrderItems.Sum(x => x.Price * x.Quantity)));
        }
    }
}
