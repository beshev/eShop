namespace EShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using EShop.Data.Common.Models;

    public class Photo : BaseModel<int>
    {
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
