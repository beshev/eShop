namespace EShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using EShop.Data.Common.Models;

    public class Image : BaseModel<int>
    {
        [Required]
        public string Base64 { get; set; }

        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int? TemplateId { get; set; }

        public virtual Template Template { get; set; }

        public int? OrderInfoId { get; set; }

        public virtual OrderInfo OrderInfo { get; set; }
    }
}
