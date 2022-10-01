namespace EShop.Web.Infrastructure.Attributes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;

    using Microsoft.AspNetCore.Http;

    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] extensions = new string[] { ".png", ".jpeg", ".jpg" };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var formFile = value as IFormFile;
            var files = value as IEnumerable<IFormFile>;

            var result = default(ValidationResult);
            if (formFile != null)
            {
                result = this.Validate(formFile);
            }
            else if (files != null)
            {
                result = this.Validate(files);
            }

            return result;
        }

        private ValidationResult Validate(IEnumerable<IFormFile> files)
        {
            foreach (var file in files)
            {
                var result = this.Validate(file);
                if (result is not null)
                {
                    return result;
                }
            }

            return ValidationResult.Success;
        }

        private ValidationResult Validate(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            if (this.extensions.Contains(extension.ToLower()) == false)
            {
                this.ErrorMessage = $"Разрешени формати: {string.Join(' ', this.extensions)}";

                return new ValidationResult(this.ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
