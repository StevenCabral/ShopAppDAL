using ShopAPP.DAL.Core;

namespace ShopAPP.DAL.Models.Categories
{
    public class CategoriesModel : BaseAddModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public bool Deleted { get; set; }
    }
}
