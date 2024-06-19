using ShopAPP.DAL.Context;
using ShopAPP.DAL.Entities;
using ShopAPP.DAL.Interfaces;
using ShopAPP.DAL.Models.Suppliers;

namespace ShopAPP.DAL.Daos
{

    public class SuppliersDb : IDbRepository<SuppliersModel, SuppliersAddModel, SuppliersUpdateModel, SuppliersRemoveModel>
    {
        private readonly ShopContext _shopContext;

        public SuppliersDb(ShopContext context)
        {
            _shopContext = context;
        }
        public List<SuppliersModel> GetEntities()
        {
            return _shopContext.Suppliers.Where(x => x.Deleted == false).
                 Select(Suppl => new SuppliersModel()
                 {
                     SupplierId = Suppl.SupplierId,
                     CompanyName = Suppl.CompanyName,
                     ContactName = Suppl.ContactName,
                     ContactTitle = Suppl.ContactTitle,
                     Address = Suppl.Address,
                     City = Suppl.City,
                     Region = Suppl.Region,
                     PostalCode = Suppl.PostalCode,
                     Country = Suppl.Country,
                     Phone = Suppl.Phone,
                     Fax = Suppl.Fax,
                 }).ToList();
        }

        public SuppliersModel GetEntityById(int id)
        {
            return _shopContext.Suppliers.Where(x => x.SupplierId == id).
                 Select(Suppl => new SuppliersModel()
                 {
                     SupplierId = Suppl.SupplierId,
                     CompanyName = Suppl.CompanyName,
                     ContactName = Suppl.ContactName,
                     ContactTitle = Suppl.ContactTitle,
                     Address = Suppl.Address,
                     City = Suppl.City,
                     Region = Suppl.Region,
                     PostalCode = Suppl.PostalCode,
                     Country = Suppl.Country,
                     Phone = Suppl.Phone,
                     Fax = Suppl.Fax,
                 }).First();
        }

        public void RemoveEntity(SuppliersRemoveModel entity)
        {
            // Encontrar la categoría que se desea eliminar en la base de datos
            var SuppliersToDelete = _shopContext.Suppliers.FirstOrDefault(x => x.SupplierId == entity.SupplierId);

            if (SuppliersToDelete != null)
            {
                // Marcar la categoría como eliminada
                SuppliersToDelete.Deleted = true;
                SuppliersToDelete.DeleteDate = DateTime.UtcNow;
                SuppliersToDelete.DeleteUser = entity.DeleteUser;  // Asignar el usuario que está eliminando la categoría

                // Puedes optar por eliminar físicamente la entidad de la base de datos si no se necesita mantener el registro
                // _shopContext.Categories.Remove(categoryToDelete);

                // Guardar los cambios en la base de datos
                _shopContext.SaveChanges();
            }
            else
            {
                // Manejar la situación donde la categoría no se encontró
                throw new Exception($"El suplidor con el ID {entity.SupplierId} no fue encontrado.");
            }
        }

        public void SaveEntity(SuppliersAddModel entity)
        {
            // Crear una nueva instancia de Categories a partir de CategoriesAddModel
            var SupplierToAdd = new Suppliers()
            {

                SupplierId = entity.SupplierId,
                CompanyName = entity.CompanyName,
                ContactName = entity.ContactName,
                ContactTitle = entity.ContactTitle,
                Address = entity.Address,
                City = entity.City,
                Region = entity.Region,
                PostalCode = entity.PostalCode,
                Country = entity.Country,
                Phone = entity.Phone,
                Fax = entity.Fax,

                Deleted = false  // Asumiendo que por defecto una categoría nueva no está eliminada
            };

            // Agregar la nueva categoría al contexto
            _shopContext.Suppliers.Add(SupplierToAdd);

            // Guardar cambios en la base de datos
            _shopContext.SaveChanges();
        }
        public void UpdateEntity(SuppliersUpdateModel entity)
        {
            // Obtener la categoría a actualizar desde la base de datos
            var SupplierToUpdate = _shopContext.Suppliers.FirstOrDefault(x => x.SupplierId == entity.SupplierId);

            if (SupplierToUpdate != null)
            {
                // Actualizar las propiedades de la categoría con los valores de CategoriesUpdateModel
                SupplierToUpdate.SupplierId = entity.SupplierId;
                SupplierToUpdate.CompanyName = entity.CompanyName;
                SupplierToUpdate.ContactName = entity.ContactName;
                SupplierToUpdate.ContactTitle = entity.ContactTitle;
                SupplierToUpdate.Address = entity.Address;
                SupplierToUpdate.City = entity.City;
                SupplierToUpdate.Region = entity.Region;
                SupplierToUpdate.PostalCode = entity.PostalCode;
                SupplierToUpdate.Country = entity.Country;
                SupplierToUpdate.Phone = entity.Phone;
                SupplierToUpdate.Fax = entity.Fax;

                // Actualizar la categoría en el contexto
                _shopContext.Suppliers.Update(SupplierToUpdate);

                // Guardar cambios en la base de datos
                _shopContext.SaveChanges();
            }
            else
            {
                // Manejar la situación donde la categoría no se encontró
                throw new Exception($"El suplidor con ID {entity.SupplierId} no pudo ser actualizado.");
            }
        }
    }
}
