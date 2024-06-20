using ShopAPP.DAL.Core;

namespace ShopAPP.DAL.Models.Orders
{
	public class OrderRemoveModel : BaseDeleteModel
	{
		public int OrderId { get; set; }
	}
}
