namespace EShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EShop.Common;
    using EShop.Data.Common.Models;

    public class Template : BaseModel<int>
    {
        public Template()
        {
            this.TemplateProducts = new HashSet<ProductTemplate>();
            this.TemplateOrders = new HashSet<OrderInfo>();
        }

        [Required]
        [MaxLength(DataConstants.TemplateNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int ImagesFixedCount { get; set; }

        public bool HasCustomText { get; set; }

        public bool IsBaseModel { get; set; }

        public int TemplateCategoryId { get; set; }

        public virtual TemplateCategory TemplateCategory { get; set; }

        public virtual IEnumerable<ProductTemplate> TemplateProducts { get; set; }

        public virtual IEnumerable<OrderInfo> TemplateOrders { get; set; }
    }
}
