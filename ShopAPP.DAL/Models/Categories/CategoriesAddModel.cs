using ShopAPP.DAL.Core;

namespace ShopAPP.DAL.Models.Categories
{
	public class CategoriesAddModel : BaseAddModel
	{
		public string CategoryName { get; set; }
		public string Description { get; set; }
	}
}
