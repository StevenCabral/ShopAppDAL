using ShopAPP.DAL.Context;
using ShopAPP.DAL.Entities;
using ShopAPP.DAL.Interfaces;
using ShopAPP.DAL.Models.Orders;

namespace ShopAPP.DAL.Daos
{

	public class ORderDb : IDbRepository<OrderModel, OrderAddModel, OrderUpdateModel, OrderRemoveModel>
	{
		private readonly ShopContext _shopContext;

		public ORderDb(ShopContext context)
		{
			_shopContext = context;
		}
		public List<OrderModel> GetEntities()
		{
			return _shopContext.Orders.Where(x => x.Deleted == false).
				 Select(Ord => new OrderModel()
				 {
					 OrderId = Ord.OrderId,
					 CustId = Ord.CustId,
					 EmpId = Ord.EmpId,
					 OrderDate = Ord.OrderDate,
					 RequiredDate = Ord.RequiredDate,
					 ShipAddress = Ord.ShipAddress,
					 ShipperId = Ord.ShipperId,
					 Freight = Ord.Freight,
					 ShipName = Ord.ShipName,
					 ShipCity = Ord.ShipCity,
					 ShipRegion = Ord.ShipRegion,
					 ShipPostalCode = Ord.ShipPostalCode,
					 ShipCountry = Ord.ShipCountry,

				 }).ToList();
		}
		public OrderModel GetEntityById(int id)
		{
			return _shopContext.Orders.Where(x => x.OrderId == id).
				 Select(Ord => new OrderModel()
				 {
					 OrderId = Ord.OrderId,
					 CustId = Ord.CustId,
					 EmpId = Ord.EmpId,
					 OrderDate = Ord.OrderDate,
					 RequiredDate = Ord.RequiredDate,
					 ShipAddress = Ord.ShipAddress,
					 ShipperId = Ord.ShipperId,
					 Freight = Ord.Freight,
					 ShipName = Ord.ShipName,
					 ShipCity = Ord.ShipCity,
					 ShipRegion = Ord.ShipRegion,
					 ShipPostalCode = Ord.ShipPostalCode,
					 ShipCountry = Ord.ShipCountry,
				 }).First();
		}
		public void RemoveEntity(OrderRemoveModel entity)
		{
			// Encontrar la categoría que se desea eliminar en la base de datos
			var OrderToDelete = _shopContext.Orders.FirstOrDefault(x => x.OrderId == entity.OrderId);

			if (OrderToDelete != null)
			{
				// Marcar la categoría como eliminada
				OrderToDelete.Deleted = true;
				OrderToDelete.DeleteDate = DateTime.UtcNow;
				OrderToDelete.DeleteUser = entity.DeleteUser;  // Asignar el usuario que está eliminando la categoría

				// Puedes optar por eliminar físicamente la entidad de la base de datos si no se necesita mantener el registro
				// _shopContext.Categories.Remove(categoryToDelete);

				// Guardar los cambios en la base de datos
				_shopContext.SaveChanges();
			}
			else
			{
				// Manejar la situación donde la categoría no se encontró
				throw new Exception($"La orden con el ID {entity.OrderId} no fue encontrada.");
			}
		}

		public void SaveEntity(OrderAddModel entity)
		{
			// Crear una nueva instancia de Categories a partir de CategoriesAddModel
			var OrderToAdd = new Orders
			{

				OrderId = entity.OrderId,
				CustId = entity.CustId,
				EmpId = entity.EmpId,
				OrderDate = entity.OrderDate,
				RequiredDate = entity.RequiredDate,
				ShipAddress = entity.ShipAddress,
				ShipperId = entity.ShipperId,
				Freight = entity.Freight,
				ShipName = entity.ShipName,
				ShipCity = entity.ShipCity,
				ShipRegion = entity.ShipRegion,
				ShipPostalCode = entity.ShipPostalCode,
				ShipCountry = entity.ShipCountry,
				Deleted = false  // Asumiendo que por defecto una categoría nueva no está eliminada
			};

			// Agregar la nueva categoría al contexto
			_shopContext.Orders.Add(OrderToAdd);

			// Guardar cambios en la base de datos
			_shopContext.SaveChanges();
		}
		public void UpdateEntity(OrderUpdateModel entity)
		{
			// Obtener la categoría a actualizar desde la base de datos
			var OrderToUpdate = _shopContext.Orders.FirstOrDefault(x => x.OrderId == entity.OrderId);

			if (OrderToUpdate != null)
			{
				// Actualizar las propiedades de la categoría con los valores de CategoriesUpdateModel
				OrderToUpdate.OrderId = entity.OrderId;
				OrderToUpdate.CustId = entity.CustId;
				OrderToUpdate.EmpId = entity.EmpId;
				OrderToUpdate.OrderDate = entity.OrderDate;
				OrderToUpdate.RequiredDate = entity.RequiredDate;
				OrderToUpdate.ShippedDate = entity.ShippedDate;
				OrderToUpdate.ShipperId = entity.ShipperId;
				OrderToUpdate.Freight = entity.Freight;
				OrderToUpdate.ShipName = entity.ShipName;
				OrderToUpdate.ShipAddress = entity.ShipAddress;
				OrderToUpdate.ShipCity = entity.ShipCity;
				OrderToUpdate.ShipRegion = entity.ShipRegion;
				OrderToUpdate.ShipPostalCode = entity.ShipPostalCode;
				OrderToUpdate.ShipCountry = entity.ShipCountry;


				// Actualizar la categoría en el contexto
				_shopContext.Orders.Update(OrderToUpdate);

				// Guardar cambios en la base de datos
				_shopContext.SaveChanges();
			}
			else
			{
				// Manejar la situación donde la categoría no se encontró
				throw new Exception($"La orden con el ID {entity.OrderId} no fue encontrada.");
			}
		}
	}
}
