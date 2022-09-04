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
            this.TemplateCategories = new HashSet<TemplateCategory>();
            this.TemplateOrders = new HashSet<OrderItem>();
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

        public virtual ICollection<TemplateCategory> TemplateCategories { get; set; }

        public virtual ICollection<OrderItem> TemplateOrders { get; set; }
    }
}
