namespace EShop.Web.ViewComponents
{
    using EShop.Services.Data.Templates;
    using EShop.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;

    public class TemplateSelectViewComponent : ViewComponent
    {
        private readonly ITemplateService templateService;

        public TemplateSelectViewComponent(ITemplateService templateService)
        {
            this.templateService = templateService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = this.templateService
                .GetAllAsync<TemplateSelectModel>()
                .GetAwaiter()
                .GetResult();

            return this.View(viewModel);
        }
    }
}
