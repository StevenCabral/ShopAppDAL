using ShopAPP.DAL.Core;

namespace ShopAPP.DAL.Models.Shippers
{
	public class ShippersAddModel : BaseAddModel
	{
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string Region { get; set; }
		public string PostalCode { get; set; }
		public int Country { get; set; }
	}
}
