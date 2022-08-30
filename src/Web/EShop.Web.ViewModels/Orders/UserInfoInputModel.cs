namespace EShop.Web.ViewModels.Orders
{
    using System.ComponentModel.DataAnnotations;

    using EShop.Common;

    public class UserInfoInputModel
    {
        [Required(ErrorMessage = ErrorMessagesConstants.RequiredField)]
        [MaxLength(DataConstants.UserFirsNameMaxLength)]
        [Display(Name = GlobalConstants.DisplayFirsName)]
        public string FirsName { get; set; }

        [Required(ErrorMessage = ErrorMessagesConstants.RequiredField)]
        [MaxLength(DataConstants.UserLastNameMaxLength)]
        [Display(Name = GlobalConstants.DisplayLastName)]
        public string LastName { get; set; }

        [Required(ErrorMessage = ErrorMessagesConstants.RequiredField)]
        [MaxLength(DataConstants.CityMaxLength)]
        [Display(Name = GlobalConstants.DisplayCity)]
        public string City { get; set; }

        [Required(ErrorMessage = ErrorMessagesConstants.RequiredField)]
        [MaxLength(DataConstants.DeliveryAddressMaxLength)]
        [Display(Name = GlobalConstants.DisplayDeliveryAddress)]
        public string DeliveryAddress { get; set; }

        [Required(ErrorMessage = ErrorMessagesConstants.RequiredField)]
        [MaxLength(DataConstants.PhoneMaxLength)]
        [Display(Name = GlobalConstants.DisplayPhone)]
        public string Phone { get; set; }

        [MaxLength(DataConstants.BullstatMaxLength)]
        [Display(Name = GlobalConstants.DisplayBullstat)]
        public string Bullstat { get; set; }

        [MaxLength(DataConstants.MallMaxLength)]
        [Display(Name = GlobalConstants.DisplayMall)]
        public string Mall { get; set; }
    }
}
