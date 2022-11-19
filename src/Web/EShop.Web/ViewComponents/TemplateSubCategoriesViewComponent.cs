namespace EShop.Web.ViewComponents
{
    using EShop.Common;
    using EShop.Services.Data.Templates;
    using EShop.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class TemplateSubCategoriesViewComponent : ViewComponent
    {
        private readonly ITemplateService templateService;

        public TemplateSubCategoriesViewComponent(ITemplateService templateService)
        {
            this.templateService = templateService;
        }

        public IViewComponentResult Invoke(int? categoryId)
        {
            var viewModel = this.templateService.GetAllSubCategoriesAsync<SelectViewModel>().GetAwaiter().GetResult();
            this.ViewData[GlobalConstants.NameOfCategory] = categoryId;

            return this.View(viewModel);
        }
    }
}
