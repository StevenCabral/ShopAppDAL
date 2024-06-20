using ShopAPP.DAL.Context;
using ShopAPP.DAL.Entities;
using ShopAPP.DAL.Interfaces;
using ShopAPP.DAL.Models.Employees;

namespace ShopAPP.DAL.Daos
{
	public class EmployeesDb : IDbRepository<EmployeesModel, EmployeesAddModel, EmployeesUpdateModel, EmployeesRemoveModel>
	{
		private readonly ShopContext _shopContext;

		public EmployeesDb(ShopContext context)
		{
			_shopContext = context;
		}
		public List<EmployeesModel> GetEntities()
		{
			return _shopContext.Employees.Where(x => x.Deleted == false).
				 Select(empl => new EmployeesModel()
				 {
					 EmpId = empl.EmpId,
					 LastName = empl.LastName,
					 FirstName = empl.FirstName,
					 Title = empl.Title,
					 TitleOfCourtesy = empl.TitleOfCourtesy,
					 BirthDate = empl.BirthDate,
					 HireDate = empl.HireDate,
					 Address = empl.Address,
					 City = empl.City,
					 Region = empl.Region,
					 PostalCode = empl.PostalCode,
					 Country = empl.Country,
					 Phone = empl.Phone,
					 MgrId = empl.MgrId,
				 }).ToList();


		}
		public EmployeesModel GetEntityById(int id)
		{
			return (EmployeesModel)_shopContext.Employees.Where(x => x.EmpId == id).
				 Select(empl => new EmployeesModel()
				 {
					 EmpId = empl.EmpId,
					 LastName = empl.LastName,
					 FirstName = empl.FirstName,
					 Title = empl.Title,
					 TitleOfCourtesy = empl.TitleOfCourtesy,
					 BirthDate = empl.BirthDate,
					 HireDate = empl.HireDate,
					 Address = empl.Address,
					 City = empl.City,
					 Region = empl.Region,
					 PostalCode = empl.PostalCode,
					 Country = empl.Country,
					 Phone = empl.Phone,
					 MgrId = empl.MgrId,
				 }).First();
		}

		public void RemoveEntity(EmployeesRemoveModel entity)
		{
			// Encontrar la categoría que se desea eliminar en la base de datos
			var EmployeesToDelete = _shopContext.Employees.FirstOrDefault(x => x.EmpId == entity.EmpId);

			if (EmployeesToDelete != null)
			{
				// Marcar la categoría como eliminada
				EmployeesToDelete.Deleted = true;
				EmployeesToDelete.DeleteDate = DateTime.UtcNow;
				EmployeesToDelete.DeleteUser = entity.DeleteUser;  // Asignar el usuario que está eliminando la categoría

				// Puedes optar por eliminar físicamente la entidad de la base de datos si no se necesita mantener el registro
				// _shopContext.Categories.Remove(categoryToDelete);

				// Guardar los cambios en la base de datos
				_shopContext.SaveChanges();
			}
			else
			{
				// Manejar la situación donde la categoría no se encontró
				throw new Exception($"El empleado con el ID {entity.EmpId} no fue encontrado.");
			}
		}

		public void SaveEntity(EmployeesAddModel entity)
		{
			// Crear una nueva instancia de Categories a partir de CategoriesAddModel
			var EmployeesToAdd = new Employees
			{
				LastName = entity.LastName,
				FirstName = entity.FirstName,
				Title = entity.Title,
				TitleOfCourtesy = entity.TitleOfCourtesy,
				BirthDate = entity.BirthDate,
				HireDate = entity.HireDate,
				Address = entity.Address,
				City = entity.City,
				Region = entity.Region,
				PostalCode = entity.PostalCode,
				Country = entity.Country,
				Phone = entity.Phone,
				MgrId = entity.MgrId,
				CreationDate = DateTime.UtcNow,
				CreationUser = entity.CreationUser,

				Deleted = false  // Asumiendo que por defecto una categoría nueva no está eliminada
			};

			// Agregar la nueva categoría al contexto
			_shopContext.Employees.Add(EmployeesToAdd);

			// Guardar cambios en la base de datos
			_shopContext.SaveChanges();
		}
		public void UpdateEntity(EmployeesUpdateModel entity)
		{
			// Obtener la categoría a actualizar desde la base de datos
			var EmployeesToUpdate = _shopContext.Employees.FirstOrDefault(x => x.EmpId == entity.EmpId);

			if (EmployeesToUpdate != null)
			{
				// Actualizar las propiedades de la categoría con los valores de CategoriesUpdateModel
				EmployeesToUpdate.LastName = entity.LastName;
				EmployeesToUpdate.FirstName = entity.FirstName;
				EmployeesToUpdate.Title = entity.Title;
				EmployeesToUpdate.TitleOfCourtesy = entity.TitleOfCourtesy;
				EmployeesToUpdate.BirthDate = DateTime.UtcNow;
				EmployeesToUpdate.HireDate = DateTime.UtcNow;
				EmployeesToUpdate.Address = entity.Address;
				EmployeesToUpdate.City = entity.City;
				EmployeesToUpdate.Region = entity.Region;
				EmployeesToUpdate.PostalCode = entity.PostalCode;
				EmployeesToUpdate.Country = entity.Country;
				EmployeesToUpdate.Phone = entity.Phone;
				EmployeesToUpdate.MgrId = entity.MgrId;
				EmployeesToUpdate.ModifyDate = entity.ModifyDate;
				EmployeesToUpdate.ModifyUser = entity.ModifyUser;

				// Actualizar la categoría en el contexto
				_shopContext.Employees.Update(EmployeesToUpdate);

				// Guardar cambios en la base de datos
				_shopContext.SaveChanges();
			}
			else
			{
				// Manejar la situación donde la categoría no se encontró
				throw new Exception($"El empleado con el ID {entity.EmpId} no fue encontrado.");
			}
		}

	}
}
