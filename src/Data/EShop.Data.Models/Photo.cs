namespace EShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using EShop.Data.Common.Models;

    public class Photo : BaseModel<int>
    {
        [Required]
        public string ImageUrl { get; set; }
    }
}
