namespace EShop.Web.ViewModels.Orders
{
    using System.ComponentModel.DataAnnotations;
    // TODO: Put Constants for on all attributes
    public class UserInfoInputModel
    {
        [MaxLength(250)]
        public string FirsName { get; set; }

        [MaxLength(250)]
        public string LastName { get; set; }

        [MaxLength(250)]
        public string City { get; set; }

        [MaxLength(500)]
        public string DeliveryAddress { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(11)]
        public string Bulstrad { get; set; }

        [MaxLength(255)]
        public string Mol { get; set; }
    }
}
