﻿namespace EShop.Web.ViewModels.Products
{
    using EShop.Data.Models;
    using EShop.Services.Mapping;

    public class ProductViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int ImagesCount { get; set; }

        public bool HasCustomText { get; set; }

        public bool IsOutOfStock { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string SecondImageUrl { get; set; }

        public string ThirdImageUrl { get; set; }

        public string ProductCategoryName { get; set; }
    }
}
