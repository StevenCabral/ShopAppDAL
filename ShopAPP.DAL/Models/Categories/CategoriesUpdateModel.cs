using ShopAPP.DAL.Core;

namespace ShopAPP.DAL.Models.Categories
{
    public class CategoriesUpdateModel : BaseUpdateModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
