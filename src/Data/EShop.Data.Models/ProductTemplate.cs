namespace EShop.Data.Models
{
    using EShop.Data.Common.Models;

    public class ProductTemplate : BaseModel<int>
    {
        public int ProdcutId { get; set; }

        public virtual Product Prodcut { get; set; }

        public int TemplateId { get; set; }

        public virtual Template Template { get; set; }
    }
}
