namespace EShop.Web.Controllers
{
    using EShop.Web.ViewModels.Templates;
    using Microsoft.AspNetCore.Mvc;

    public class TemplatesController : BaseController
    {
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(TemplateInputModel model)
        {
            return this.RedirectToAction(nameof(this.Add));
        }
    }
}
