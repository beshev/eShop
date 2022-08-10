﻿namespace EShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EShop.Data.Common.Models;

    public class TemplateCategory : BaseModel<int>
    {
        public TemplateCategory()
        {
            this.Templates = new HashSet<Template>();
        }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public virtual IEnumerable<Template> Templates { get; set; }
    }
}