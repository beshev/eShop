namespace EShop.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        public IActionResult Index()
        {
            // TODO: Take items count
            return this.View();
        }
    }
}
