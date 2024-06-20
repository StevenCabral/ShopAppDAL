using ShopAPP.DAL.Core;
using System.ComponentModel.DataAnnotations;

namespace ShopAPP.DAL.Entities
{
	public class Shippers : BaseEntity
	{
		[Key]
		public int ShipperId { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string Region { get; set; }
		public string PostalCode { get; set; }
		public int Country { get; set; }
	}
}
