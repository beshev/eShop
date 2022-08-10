namespace EShop.Data.Models
{
    using EShop.Data.Common.Models;

    public class ProductColor : BaseModel<int>
    {
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public virtual int ColordId { get; set; }

        public virtual Color Colord { get; set; }
    }
}
