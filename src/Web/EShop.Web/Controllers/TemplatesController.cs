namespace EShop.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EShop.Common;
    using EShop.Services.Data.Products;
    using EShop.Services.Data.Templates;
    using EShop.Web.ViewModels.Products;
    using EShop.Web.ViewModels.Templates;
    using Microsoft.AspNetCore.Mvc;

    public class TemplatesController : BaseController
    {
        private const int TemplatesPerPage = 12;
        private readonly ITemplateService templateService;
        private readonly IProductService productService;

        public TemplatesController(
            ITemplateService templateService,
            IProductService productService)
        {
            this.templateService = templateService;
            this.productService = productService;
        }

        public async Task<IActionResult> All(int id = 1, int? productId = null, int? categoryId = null)
        {
            if (productId.HasValue == false)
            {
                return this.NotFound();
            }

            if (id < 1)
            {
                return this.NotFound();
            }

            int count = await this.templateService.GetCountAsync();
            int pagesCount = (int)Math.Ceiling((double)count / TemplatesPerPage);

            if (pagesCount != 0 && id > pagesCount)
            {
                return this.NotFound();
            }

            try
            {
                var skip = (id - 1) * TemplatesPerPage;
                var viewModel = new AllTemplatesViewModel
                {
                    PageNumber = id,
                    PagesCount = pagesCount,
                    ForAction = nameof(this.All),
                    ForController = this.GetType().Name.Replace(nameof(Controller), string.Empty),
                    Templates = await this.templateService.GetAllAsync<TemplateBaseViewModel>(productId, categoryId, skip, TemplatesPerPage),
                    Product = await this.productService.GetByIdAsync<ProductSelectModel>(productId.Value),
                };

                if (viewModel.Templates.Any() == false)
                {
                    return this.NotFound();
                }

                return this.View(viewModel);
            }
            catch (Exception)
            {
                return this.RedirectToAction(GlobalConstants.ErrorAction, GlobalConstants.HomeController);
            }
        }

        public async Task<IActionResult> Details(int templateId, int productId)
        {
            if (await this.templateService.IsCompatibleWithProductAsync(templateId, productId) == false)
            {
                return this.NotFound();
            }

            try
            {
                var viewModel = await this.templateService.GetByIdAsync<TemplateViewModel>(templateId);
                viewModel.ProductId = productId;
                return this.View(viewModel);
            }
            catch (Exception)
            {
                return this.RedirectToAction(GlobalConstants.ErrorAction, GlobalConstants.HomeController);
            }
        }
    }
}
