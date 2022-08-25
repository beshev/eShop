namespace EShop.Web.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class BaseController : Controller
    {
        public ISession Session => this.HttpContext.Session;
    }
}
