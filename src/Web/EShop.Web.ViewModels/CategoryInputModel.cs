namespace EShop.Web.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EShop.Common;

    public class CategoryInputModel : IValidatableObject
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public IEnumerable<int> TemplatesIds { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(this.Name) || this.Name.Length > DataConstants.CategoryNameMaxLength)
            {
                yield return new ValidationResult(string.Format(ErrorMessagesConstants.CategoryErrorMessage, DataConstants.CategoryNameMaxLength));
            }
        }
    }
}
