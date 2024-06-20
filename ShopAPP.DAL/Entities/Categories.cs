using ShopAPP.DAL.Core;
using System.ComponentModel.DataAnnotations;

namespace ShopAPP.DAL.Entities
{
	public class Categories : BaseEntity
	{
		[Key]
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public string Description { get; set; }
	}
}
