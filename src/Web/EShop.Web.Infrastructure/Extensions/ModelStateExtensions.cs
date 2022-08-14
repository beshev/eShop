namespace EShop.Web.Infrastructure.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public static class ModelStateExtensions
    {
        public static IEnumerable<string> GetModelStateErrors(this ModelStateDictionary modelState)
        {
            return modelState
                .Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage);
        }
    }
}
