namespace ShopAPP.DAL.Core
{
	public abstract class BaseDeleteModel
	{
		public DateTime? DeleteDate { get; set; }
		public int? DeleteUser { get; set; }
		public bool Deleted { get; set; }
	}
}
