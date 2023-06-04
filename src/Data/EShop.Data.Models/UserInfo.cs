namespace EShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using EShop.Common;
    using EShop.Data.Common.Models;
    using EShop.Data.Models.Enums;

    public class UserInfo : BaseModel<int>
    {
        [Required]
        [MaxLength(DataConstants.UserFirsNameMaxLength)]

        public string FirstName { get; set; }

        [Required]
        [MaxLength(DataConstants.UserLastNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(DataConstants.CityMaxLength)]
        public string City { get; set; }

        [Required]
        [MaxLength(DataConstants.DeliveryAddressMaxLength)]
        public string DeliveryAddress { get; set; }

        public AddressType DeliveryAddressType { get; set; }

        public Carrier Carrier { get; set; }

        [Required]
        [MaxLength(DataConstants.PhoneMaxLength)]
        public string Phone { get; set; }

        [MaxLength(DataConstants.CompanyNameMaxLength)]
        public string CompanyName { get; set; }

        [MaxLength(DataConstants.BullstatMaxLength)]
        public string Bullstat { get; set; }

        [MaxLength(DataConstants.MallMaxLength)]
        public string Mall { get; set; }

        [MaxLength(DataConstants.CompanyAddressMaxLength)]
        public string CompanyAddress { get; set; }

        public bool IsRegisteredByVAT { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
