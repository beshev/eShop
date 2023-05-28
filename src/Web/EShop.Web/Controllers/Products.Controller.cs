namespace EShop.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EShop.Common;
    using EShop.Services.Data.Products;
    using EShop.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    [ResponseCache(CacheProfileName = GlobalConstants.ItemsCacheProfileName)]
    public class Products : BaseController
    {
        private const int ProductsPerPage = 12;
        private readonly IProductService productService;
        private readonly IMemoryCache memoryCache;

        public Products(
            IProductService productService,
            IMemoryCache memoryCache)
        {
            this.productService = productService;
            this.memoryCache = memoryCache;
        }

        public async Task<IActionResult> All(int? categoryId, int id = 1)
        {
            if (id < 0)
            {
                return this.NotFound();
            }

            int count = await this.productService.GetCountAsync(categoryId);
            int pagesCount = (int)Math.Ceiling((double)count / ProductsPerPage);

            if (pagesCount != 0 && id > pagesCount)
            {
                return this.NotFound();
            }

            var skip = (id - 1) * ProductsPerPage;
            var viewModel = new AllProductsViewModel
            {
                PageNumber = id,
                PagesCount = pagesCount,
                ForAction = nameof(this.All),
                ForController = this.GetType().Name.Replace(nameof(Controller), string.Empty),
                Products = await this.productService.GetAllAsync<ProductViewModel>(skip, ProductsPerPage, categoryId),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int productId)
        {
            var productCacheKey = string.Format(GlobalConstants.ProductCacheKey, productId);
            if (!this.memoryCache.TryGetValue(productCacheKey, out ProductDetailsModel viewModel))
            {
                viewModel = await this.productService.GetByIdAsync<ProductDetailsModel>(productId);
                this.memoryCache.Set(productCacheKey, viewModel, TimeSpan.FromSeconds(GlobalConstants.CacheExpirationTimeInSeconds));
            }

            this.ViewData[GlobalConstants.ReturnUrlKey] = this.ReturnUrl;

            this.TempData[GlobalConstants.NameOfOrderProductId] = productId;

            return this.View(viewModel);
        }
    }
}
