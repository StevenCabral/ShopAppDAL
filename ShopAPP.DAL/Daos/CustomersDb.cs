using ShopAPP.DAL.Context;
using ShopAPP.DAL.Entities;
using ShopAPP.DAL.Interfaces;
using ShopAPP.DAL.Models.Customers;

namespace ShopAPP.DAL.Daos
{
	public class CustomersDb : IDbRepository<CustomersModel, CustomersAddModel, CustomersUpdateModel, CustomersRemoveModel>
	{
		private readonly ShopContext _shopContext;

		public CustomersDb(ShopContext context)
		{
			_shopContext = context;
		}
		public List<CustomersModel> GetEntities()
		{
			return _shopContext.Customers.Where(x => x.Deleted == false).
				 Select(cus => new CustomersModel()
				 {
					 Address = cus.Address,
					 City = cus.City,
					 PostalCode = cus.PostalCode,
					 Country = cus.Country,
					 CompanyName = cus.CompanyName,
					 ContactName = cus.ContactName,
					 ContactTitle = cus.ContactTitle,
					 CustId = cus.CustId,
					 Email = cus.Email,
					 Fax = cus.Fax,
					 Phone = cus.Phone,
					 Region = cus.Region,
					 CreationDate = cus.CreationDate,
					 CreationUser = cus.CreationUser
				 }).ToList();
		}

		public CustomersModel GetEntityById(int id)
		{
			return _shopContext.Customers.Where(x => x.CustId == id).
				 Select(cus => new CustomersModel()
				 {
					 Address = cus.Address,
					 City = cus.City,
					 PostalCode = cus.PostalCode,
					 Country = cus.Country,
					 CompanyName = cus.CompanyName,
					 ContactName = cus.ContactName,
					 ContactTitle = cus.ContactTitle,
					 CustId = cus.CustId,
					 Email = cus.Email,
					 Fax = cus.Fax,
					 Phone = cus.Phone,
					 Region = cus.Region,
					 CreationDate = cus.CreationDate,
					 CreationUser = cus.CreationUser
				 }).First();
		}

		public void RemoveEntity(CustomersRemoveModel entity)
		{
			// Encontrar la categoría que se desea eliminar en la base de datos
			var categoryToDelete = _shopContext.Customers.FirstOrDefault(x => x.CustId == entity.CustId);

			if (categoryToDelete != null)
			{
				// Marcar la categoría como eliminada
				categoryToDelete.Deleted = true;
				categoryToDelete.DeleteDate = DateTime.UtcNow;
				categoryToDelete.DeleteUser = entity.DeleteUser;  // Asignar el usuario que está eliminando la categoría

				// Puedes optar por eliminar físicamente la entidad de la base de datos si no se necesita mantener el registro
				// _shopContext.Categories.Remove(categoryToDelete);

				// Guardar los cambios en la base de datos
				_shopContext.SaveChanges();
			}
			else
			{
				// Manejar la situación donde la categoría no se encontró
				throw new Exception($"La categoria con el ID {entity.CustId} no fue encontrada.");
			}
		}

		public void SaveEntity(CustomersAddModel entity)
		{
			// Crear una nueva instancia de Categories a partir de CategoriesAddModel
			var customerToAdd = new Customers
			{

				CompanyName = entity.CompanyName,
				ContactName = entity.ContactName,
				ContactTitle = entity.ContactTitle,
				Address = entity.Address,
				Email = entity.Email,
				City = entity.City,
				Region = entity.Region,
				PostalCode = entity.PostalCode,
				Country = entity.Country,
				Phone = entity.Phone,
				Fax = entity.Fax,
				CreationDate = DateTime.UtcNow,
				CreationUser = entity.CreationUser,
				Deleted = false  // Asumiendo que por defecto una categoría nueva no está eliminada
			};

			// Agregar la nueva categoría al contexto
			_shopContext.Customers.Add(customerToAdd);

			// Guardar cambios en la base de datos
			_shopContext.SaveChanges();
		}

		public void UpdateEntity(CustomersUpdateModel entity)
		{
			// Obtener la categoría a actualizar desde la base de datos
			var customerToUpdate = _shopContext.Customers.FirstOrDefault(x => x.CustId == entity.CustId);

			if (customerToUpdate != null)
			{
				// Actualizar las propiedades de la categoría con los valores de CategoriesUpdateModel
				customerToUpdate.CompanyName = entity.CompanyName;
				customerToUpdate.ContactName = entity.ContactName;
				customerToUpdate.ContactTitle = entity.ContactTitle;
				customerToUpdate.Address = entity.Address;
				customerToUpdate.Email = entity.Email;
				customerToUpdate.City = entity.City;
				customerToUpdate.Region = entity.Region;
				customerToUpdate.PostalCode = entity.PostalCode;
				customerToUpdate.Country = entity.Country;
				customerToUpdate.Phone = entity.Phone;
				customerToUpdate.Fax = entity.Fax;
				customerToUpdate.ModifyDate = DateTime.UtcNow;
				customerToUpdate.ModifyUser = entity.ModifyUser;

				// Actualizar la categoría en el contexto
				_shopContext.Customers.Update(customerToUpdate);

				// Guardar cambios en la base de datos
				_shopContext.SaveChanges();
			}
			else
			{
				// Manejar la situación donde la categoría no se encontró
				throw new Exception($"Category with ID {entity.CustId} not found.");
			}
		}
	}
}
