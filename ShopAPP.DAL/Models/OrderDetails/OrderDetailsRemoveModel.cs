using ShopAPP.DAL.Core;

namespace ShopAPP.DAL.Models.OrderDetails
{
	public class OrderDetailsRemoveModel : BaseDeleteModel
	{
		public int OrderId { get; set; }
	}
}
