namespace EShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using EShop.Services.Data.Templates;
    using EShop.Web.ViewModels.Templates;
    using Microsoft.AspNetCore.Mvc;

    public class TemplatesController : AdministrationController
    {
        private readonly ITemplateService templateService;

        public TemplatesController(ITemplateService templateService)
        {
            this.templateService = templateService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(TemplateInputModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            await this.templateService.AddAsync(model.Name, model.Description, model.Price, model.Image, model.ImagesFixedCount, model.IsBaseModel, model.HasCustomText, model.TemplateCategoryId, model.ForProducts);

            return this.RedirectToAction(nameof(this.Add));
        }
    }
}
