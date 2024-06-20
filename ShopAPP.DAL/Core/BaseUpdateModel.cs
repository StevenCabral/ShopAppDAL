namespace ShopAPP.DAL.Core
{
	public abstract class BaseUpdateModel
	{
		public DateTime? ModifyDate { get; set; }
		public int? ModifyUser { get; set; }
	}
}
