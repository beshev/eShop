namespace EShop.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class TemplatesController : BaseController
    {
        public async Task<IActionResult> All(int id, string productId = null, string categoryId = null)
        {
            return this.View();
        }
    }
}
