namespace EShop.Web.ViewModels.Orders
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using EShop.Common;
    using EShop.Data.Models;
    using EShop.Services.Mapping;

    public class OrderItemViewModel : IMapFrom<OrderItem>, IHaveCustomMappings
    {
        [Display(Name = GlobalConstants.NameOfPrice)]
        public decimal Price { get; set; }

        [Display(Name = GlobalConstants.NameOfDescription)]
        public string Description { get; set; }

        [Display(Name = GlobalConstants.NameOfQuantity)]
        public int Quantity { get; set; }

        [Display(Name = GlobalConstants.NameOfFontStyle)]
        public int? FontStyle { get; set; }

        public IEnumerable<string> Images { get; set; }

        [Display(Name = GlobalConstants.NameOfProducts)]
        public string ProductName { get; set; }

        public string TemplateName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<OrderItem, OrderItemViewModel>()
                .ForMember(dest => dest.Images, src => src.MapFrom(x => x.Images.Select(image => image.ImageUrl)));
        }
    }
}
