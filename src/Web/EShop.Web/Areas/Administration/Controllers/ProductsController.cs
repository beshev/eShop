namespace EShop.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EShop.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : AdministrationController
    {
        public async Task<IActionResult> All()
        {
            var model = new List<ProductViewModel>();

            return this.View(model);
        }

        public async Task<IActionResult> Add()
        {
            return this.View();
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
