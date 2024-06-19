namespace ShopAPP.DAL.Core
{
    public abstract class BaseAddModel
    {
        public DateTime CreationDate { get; set; }
        public int CreationUser { get; set; }
    }
}
