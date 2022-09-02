namespace EShop.Web.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class BaseController : Controller
    {
        public ISession Session => this.HttpContext.Session;

        public string ReturnUrl => this.HttpContext.Request.Path.Value + this.HttpContext.Request.QueryString.Value;
    }
}
