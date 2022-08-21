namespace EShop.Web.Controllers
{
    using System.Linq;
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
            if (productId.HasValue == false)
            {
                return this.NotFound();
            }

            var viewModel = new ProductTemplatesViewModel
            {
                Templates = await this.templateService.GetAllAsync<TemplateBaseViewModel>(productId, categoryId),
                Product = await this.productService.GetByIdAsync<ProductSelectModel>(productId.Value),
            };

            if (viewModel.Templates.Any() == false)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int templateId, int productId)
        {
            if (await this.templateService.IsCompatibleWithProductAsync(templateId, productId) == false)
            {
                return this.NotFound();
            }

            var viewModel = await this.templateService.GetByIdAsync<TemplateViewModel>(templateId);
            viewModel.ProductId = productId;
            return this.View(viewModel);
        }

        public IActionResult AddToCart(OrderInfoInputModel model)
        {
            model.ProductName = this.TempData[GlobalConstants.NameOfOrderProductName] as string;
            model.Price = decimal.Parse(this.TempData[GlobalConstants.NameOfOrderPrice] as string);
            model.TemplateId = this.TempData[GlobalConstants.NameOfOrderTemplateId] as int?;
            model.ProductId = this.TempData[GlobalConstants.NameOfOrderProductId] as int?;

            if (model.TemplateId is null || model.ProductId is null)
            {
                return this.NotFound();
            }

            return this.RedirectToAction(nameof(this.All), new { model.ProductId });
        }
    }
}
