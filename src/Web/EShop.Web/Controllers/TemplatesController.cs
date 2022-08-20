namespace EShop.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EShop.Services.Data.Products;
    using EShop.Services.Data.Templates;
    using EShop.Web.ViewModels.Products;
    using EShop.Web.ViewModels.Templates;
    using Microsoft.AspNetCore.Mvc;

    public class TemplatesController : BaseController
    {
        private readonly ITemplateService templateService;
        private readonly IProductService productService;

        public TemplatesController(
            ITemplateService templateService,
            IProductService productService)
        {
            this.templateService = templateService;
            this.productService = productService;
        }

        public async Task<IActionResult> All(int id, int? productId = null, int? categoryId = null)
        {
            var viewModel = new ProductTemplatesViewModel
            {
                Templates = await this.templateService.GetAllAsync<TemplateViewModel>(productId, categoryId),
                Product = await this.productService.GetByIdAsync<ProductSelectModel>(productId.Value),
            };

            return this.View(viewModel);
        }
    }
}
