using Microsoft.EntityFrameworkCore;
using ShopAPP.DAL.Entities;

namespace ShopAPP.DAL.Context
{
	public class ShopContext : DbContext
	{
		#region Constructor

		public ShopContext(DbContextOptions<ShopContext> options) : base(options)
		{ }

		#endregion

		#region DbSets
		public DbSet<Categories> Categories { get; set; }
		public DbSet<Customers> Customers { get; set; }
		public DbSet<Employees> Employees { get; set; }
		public DbSet<OrderDetails> OrderDetails { get; set; }
		public DbSet<Orders> Orders { get; set; }
		public DbSet<Products> Products { get; set; }
		public DbSet<Shippers> Shippers { get; set; }
		public DbSet<Suppliers> Suppliers { get; set; }

		#endregion
	}
}
