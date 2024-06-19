using ShopAPP.DAL.Core;

namespace ShopAPP.DAL.Entities
{
    public class Categories : BaseEntity
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
