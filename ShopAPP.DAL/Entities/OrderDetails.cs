namespace ShopAPP.DAL.Entities
{
    public class OrderDetails
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; } = 0;
        public short Qty { get; set; } = 1;
        public decimal Discount { get; set; } = 0;
    }
}
