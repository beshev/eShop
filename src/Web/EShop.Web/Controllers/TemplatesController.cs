namespace EShop.Web.Controllers
{
    using System.Threading.Tasks;
    using EShop.Common;
    using EShop.Services.Data.Products;
    using EShop.Services.Data.Templates;
    using EShop.Web.ViewModels.Orders;
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
                Templates = await this.templateService.GetAllAsync<TemplateBaseViewModel>(productId, categoryId),
                Product = await this.productService.GetByIdAsync<ProductSelectModel>(productId.Value),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int templateId)
        {
            var viewModel = await this.templateService.GetByIdAsync<TemplateViewModel>(templateId);

            if (viewModel is null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public IActionResult AddToCart(OrderInfoInputModel model)
        {
            model.ProductName = this.TempData[GlobalConstants.NameOfOrderProductName] as string;
            model.Price = decimal.Parse(this.TempData[GlobalConstants.NameOfOrderPrice] as string);
            model.TemplateId = this.TempData[GlobalConstants.NameOfOrderTemplateId] as int?;
            model.ProductId = this.TempData[GlobalConstants.NameOfOrderProductId] as int?;

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
