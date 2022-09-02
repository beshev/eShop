namespace EShop.Web.ViewModels.Orders
{
    public class OrderItemViewComponentModel
    {
        public OrderItemInputModel OrderInfo { get; set; }

        public int ImagesCount { get; set; }

        public string ReturnUrl { get; set; }

        public bool HasCustomText { get; set; }
    }
}
