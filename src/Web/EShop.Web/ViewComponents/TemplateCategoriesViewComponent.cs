namespace EShop.Web.ViewComponents
{
    using EShop.Services.Data.Templates;
    using EShop.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class TemplateCategoriesViewComponent : ViewComponent
    {
        private readonly ITemplateService templateService;

        public TemplateCategoriesViewComponent(ITemplateService templateService)
        {
            this.templateService = templateService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = this.templateService.GetCategoriesAsync<SelectViewModel>().GetAwaiter().GetResult();
            return this.View(viewModel);
        }
    }
}
