using ShopAPP.DAL.Core;

namespace ShopAPP.DAL.Models.OrderDetails
{
	public class OrderDetailsAddModel : BaseAddModel
	{
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		public decimal UnitPrice { get; set; }
		public short Qty { get; set; }
		public decimal Discount { get; set; }
		public bool Deleted { get; set; }
	}
}
