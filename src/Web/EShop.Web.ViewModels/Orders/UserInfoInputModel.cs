namespace EShop.Web.ViewModels.Orders
{
    using System.ComponentModel.DataAnnotations;

    using EShop.Common;

    public class UserInfoInputModel
    {
        [Required]
        [MaxLength(DataConstants.UserFirsNameMaxLength)]
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
    }
}
