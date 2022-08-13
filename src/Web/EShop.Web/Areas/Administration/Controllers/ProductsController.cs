namespace EShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : AdministrationController
    {
        public async Task<IActionResult> All()
        {
            return this.View();
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
