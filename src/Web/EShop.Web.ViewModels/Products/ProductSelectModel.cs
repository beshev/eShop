namespace EShop.Web.ViewModels.Products
{
    using System.ComponentModel.DataAnnotations;

    using EShop.Common;
    using EShop.Data.Models;
    using EShop.Services.Mapping;

    public class ProductSelectModel : SelectViewModel, IMapFrom<Product>
    {
        [Display(Name = GlobalConstants.NameOfProducts)]
        public override string Name { get => base.Name; set => base.Name = value; }
    }
}
