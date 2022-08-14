namespace EShop.Web.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EShop.Common;

    public class CategoryInputModel : IValidatableObject
    {
        public string CategoryName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(this.CategoryName) || this.CategoryName.Length > DataConstants.CategoryNameMaxLength)
            {
                yield return new ValidationResult(string.Format(ErrorMessagesConstants.CategoryErrorMessage, DataConstants.CategoryNameMaxLength));
            }
        }
    }
}
