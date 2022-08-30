namespace EShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using EShop.Common;
    using EShop.Data.Common.Models;

    public class UserInfo : BaseModel<int>
    {
        [Required]
        [MaxLength(DataConstants.UserFirsNameMaxLength)]

        // TODO: Fix that name
        public string FirsName { get; set; }

        [Required]
        [MaxLength(DataConstants.UserLastNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(DataConstants.CityMaxLength)]
        public string City { get; set; }

        [Required]
        [MaxLength(DataConstants.DeliveryAddressMaxLength)]
        public string DeliveryAddress { get; set; }

        [Required]
        [MaxLength(DataConstants.PhoneMaxLength)]
        public string Phone { get; set; }

        [MaxLength(DataConstants.BullstatMaxLength)]
        public string Bullstat { get; set; }

        [MaxLength(DataConstants.MallMaxLength)]
        public string Mall { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
