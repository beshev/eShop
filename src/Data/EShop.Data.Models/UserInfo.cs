namespace EShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using EShop.Common;
    using EShop.Data.Common.Models;

    public class UserInfo : BaseModel<int>
    {
        [Required]
        [MaxLength(GlobalConstants.UserFirsNameMaxLength)]
        public string FirsName { get; set; }

        [Required]
        [MaxLength(GlobalConstants.UserLastNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(GlobalConstants.CityMaxLength)]
        public string City { get; set; }

        [Required]
        [MaxLength(GlobalConstants.DeliveryAddressMaxLength)]
        public string DeliveryAddress { get; set; }

        [Required]
        [MaxLength(GlobalConstants.PhoneMaxLength)]
        public string Phone { get; set; }

        [MaxLength(GlobalConstants.BullstatMaxLength)]
        public string Bullstat { get; set; }

        [MaxLength(GlobalConstants.MallMaxLength)]
        public string Mall { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
