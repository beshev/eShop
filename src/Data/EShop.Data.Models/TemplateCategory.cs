namespace EShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EShop.Common;
    using EShop.Data.Common.Models;

    // TODO: Add category image
    public class TemplateCategory : BaseModel<int>
    {
        public TemplateCategory()
        {
            this.Templates = new HashSet<Template>();
        }

        [Required]
        [MaxLength(DataConstants.CategoryNameMaxLength)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<Template> Templates { get; set; }
    }
}
