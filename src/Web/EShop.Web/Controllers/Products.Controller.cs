namespace EShop.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class Products : BaseController
    {
        public async Task<IActionResult> All()
        {
            return this.View();
        }
    }
}
