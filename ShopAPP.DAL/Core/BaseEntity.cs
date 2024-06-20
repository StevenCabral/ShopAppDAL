using System.ComponentModel.DataAnnotations.Schema;

namespace ShopAPP.DAL.Core
{
	public abstract class BaseEntity
	{
		[Column("creation_date")]
		public DateTime CreationDate { get; set; }

		[Column("creation_user")]
		public int CreationUser { get; set; }

		[Column("modify_date")]
		public DateTime? ModifyDate { get; set; }

		[Column("modify_user")]
		public int? ModifyUser { get; set; }

		[Column("delete_date")]
		public DateTime? DeleteDate { get; set; }

		[Column("delete_user")]
		public int? DeleteUser { get; set; }
		public bool Deleted { get; set; }
	}
}
