namespace EShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EShop.Data.Common.Models;

    public class Color : BaseModel<int>
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public virtual IEnumerable<ProductColor> ColorProducts { get; set; }
    }
}
