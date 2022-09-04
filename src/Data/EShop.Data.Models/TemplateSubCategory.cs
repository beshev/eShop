namespace EShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EShop.Common;
    using EShop.Data.Common.Models;

    public class TemplateSubCategory : BaseModel<int>
    {
        public TemplateSubCategory()
        {
            this.Templates = new HashSet<Template>();
        }

        [Required]
        [MaxLength(DataConstants.CategoryNameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<Template> Templates { get; set; }
    }
}
