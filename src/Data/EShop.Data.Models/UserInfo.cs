namespace EShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using EShop.Data.Common.Models;

    public class UserInfo : BaseModel<int>
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

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
