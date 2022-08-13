namespace EShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using EShop.Services.Data.Products;
    using EShop.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : AdministrationController
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> All()
        {
            var model = await this.productService.GetAllAsync<ProductViewModel>();

            return this.View(model);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductInputModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            await this.productService.AddAsync(model.Name, model.Price, model.Description, model.ProductCategoryId, model.HasCustomText, model.Image);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            return this.View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            return this.View();
        }

        public async Task<IActionResult> AddCategory()
        {
            return this.View();
        }
    }
}
