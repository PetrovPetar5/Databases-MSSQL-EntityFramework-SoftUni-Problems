namespace ProductShop.DataTransferObjects
{
    public class UserInputProduct
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
        public int SellerId { get; set; }
        public int? BuyerId { get; set; }
    }
}
