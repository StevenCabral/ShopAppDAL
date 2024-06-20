using ShopAPP.DAL.Core;

namespace ShopAPP.DAL.Models.Products
{
	public class ProductsAddModel : BaseAddModel
	{
		public string ProductName { get; set; }
		public int SupplierId { get; set; }
		public int CategoryId { get; set; }
		public decimal UnitPrice { get; set; }
		public bool Discontinued { get; set; }
		public bool Deleted { get; set; }
	}
}
