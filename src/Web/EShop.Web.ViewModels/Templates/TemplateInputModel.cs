﻿namespace EShop.Web.ViewModels.Templates
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class TemplateInputModel
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }

        public int ImagesFixedCount { get; set; }

        public bool HasCustomText { get; set; }

        public bool IsBaseModel { get; set; }

        public int TemplateCategoryId { get; set; }
    }
}
