using ShopAPP.DAL.Context;
using ShopAPP.DAL.Entities;
using ShopAPP.DAL.Interfaces;
using ShopAPP.DAL.Models.Shippers;

namespace ShopAPP.DAL.Daos
{

	public class ShipperDb : IDbRepository<ShippersModel, ShippersAddModel, ShippersUpdateModel, ShippersRemoveModel>
	{
		private readonly ShopContext _shopContext;

		public ShipperDb(ShopContext context)
		{
			_shopContext = context;
		}
		public List<ShippersModel> GetEntities()
		{
			return _shopContext.Shippers.Where(x => x.Deleted == false).
				 Select(Shipper => new ShippersModel()
				 {
					 ShipperId = Shipper.ShipperId,
					 Name = Shipper.Name,
					 Phone = Shipper.Phone,
					 Address = Shipper.Address,
					 City = Shipper.City,
					 Region = Shipper.Region,
					 PostalCode = Shipper.PostalCode,
					 Country = Shipper.Country,
				 }).ToList();
		}

		public ShippersModel GetEntityById(int id)
		{
			return _shopContext.Shippers.Where(x => x.ShipperId == id).
				 Select(Shipper => new ShippersModel()
				 {
					 ShipperId = Shipper.ShipperId,
					 Name = Shipper.Name,
					 Phone = Shipper.Phone,
					 Address = Shipper.Address,
					 City = Shipper.City,
					 Region = Shipper.Region,
					 PostalCode = Shipper.PostalCode,
					 Country = Shipper.Country,
				 }).First();
		}

		public void RemoveEntity(ShippersRemoveModel entity)
		{
			// Encontrar la categoría que se desea eliminar en la base de datos
			var ShipperToDelete = _shopContext.Shippers.FirstOrDefault(x => x.ShipperId == entity.ShipperId);

			if (ShipperToDelete != null)
			{
				// Marcar la categoría como eliminada
				ShipperToDelete.Deleted = true;
				ShipperToDelete.DeleteDate = DateTime.UtcNow;
				ShipperToDelete.DeleteUser = entity.DeleteUser;  // Asignar el usuario que está eliminando la categoría

				// Puedes optar por eliminar físicamente la entidad de la base de datos si no se necesita mantener el registro
				// _shopContext.Categories.Remove(categoryToDelete);

				// Guardar los cambios en la base de datos
				_shopContext.SaveChanges();
			}
			else
			{
				// Manejar la situación donde la categoría no se encontró
				throw new Exception($"El mensajero con el ID {entity.ShipperId} no fue encontrado.");
			}
		}

		public void SaveEntity(ShippersAddModel entity)
		{
			// Crear una nueva instancia de Categories a partir de CategoriesAddModel
			var ShipperToAdd = new Shippers
			{

				Name = entity.Name,
				Phone = entity.Phone,
				Address = entity.Address,
				City = entity.City,
				Region = entity.Region,
				PostalCode = entity.PostalCode,
				Country = entity.Country,
				CreationDate = entity.CreationDate,
				CreationUser = entity.CreationUser,

				Deleted = false  // Asumiendo que por defecto una categoría nueva no está eliminada
			};

			// Agregar la nueva categoría al contexto
			_shopContext.Shippers.Add(ShipperToAdd);

			// Guardar cambios en la base de datos
			_shopContext.SaveChanges();
		}

		public void UpdateEntity(ShippersUpdateModel entity)
		{
			// Obtener la categoría a actualizar desde la base de datos
			var ShipperToUpdate = _shopContext.Shippers.FirstOrDefault(x => x.ShipperId == entity.ShipperId);

			if (ShipperToUpdate != null)
			{
				// Actualizar las propiedades de la categoría con los valores de CategoriesUpdateModel
				ShipperToUpdate.Name = entity.Name;
				ShipperToUpdate.Phone = entity.Phone;
				ShipperToUpdate.Address = entity.Address;
				ShipperToUpdate.City = entity.City;
				ShipperToUpdate.Region = entity.Region;
				ShipperToUpdate.PostalCode = entity.PostalCode;
				ShipperToUpdate.Country = entity.Country;
				ShipperToUpdate.ModifyUser = entity.ModifyUser;
				ShipperToUpdate.ModifyDate = entity.ModifyDate;


				// Actualizar la categoría en el contexto
				_shopContext.Shippers.Update(ShipperToUpdate);

				// Guardar cambios en la base de datos
				_shopContext.SaveChanges();
			}
			else
			{
				// Manejar la situación donde la categoría no se encontró
				throw new Exception($"EL mensajero con el ID {entity.ShipperId} no pudo ser actualizado.");
			}
		}


	}

}
