using ShopAPP.DAL.Context;
using ShopAPP.DAL.Entities;
using ShopAPP.DAL.Interfaces;
using ShopAPP.DAL.Models.Products;

namespace ShopAPP.DAL.Daos
{
    public class ProductsDb : IDbRepository<ProductsModel, ProductsAddModel, ProductsUpdateModel, ProductsRemoveModel>
    {
        private readonly ShopContext _shopContext;

        public ProductsDb(ShopContext context)
        {
            _shopContext = context;
        }

        public List<ProductsModel> GetEntities()
        {
            return _shopContext.Products.Where(x => x.Deleted == false).
                 Select(Prod => new ProductsModel()
                 {
                     ProductId = Prod.ProductId,
                     ProductName = Prod.ProductName,
                     SupplierId = Prod.SupplierId,
                     CategoryId = Prod.CategoryId,
                     UnitPrice = Prod.UnitPrice,
                     Discontinued = Prod.Discontinued,

                 }).ToList();
        }

        public ProductsModel GetEntityById(int id)
        {
            return _shopContext.Products.Where(x => x.ProductId == id).
                 Select(Prod => new ProductsModel()
                 {
                     ProductId = Prod.ProductId,
                     ProductName = Prod.ProductName,
                     SupplierId = Prod.SupplierId,
                     CategoryId = Prod.CategoryId,
                     UnitPrice = Prod.UnitPrice,
                     Discontinued = Prod.Discontinued,

                 }).First();
        }

        public void RemoveEntity(ProductsRemoveModel entity)
        {
            // Encontrar la categoría que se desea eliminar en la base de datos
            var ProductToDelete = _shopContext.Products.FirstOrDefault(x => x.ProductId == entity.ProductId);

            if (ProductToDelete != null)
            {
                // Marcar la categoría como eliminada
                ProductToDelete.Deleted = true;
                ProductToDelete.DeleteDate = DateTime.UtcNow;
                ProductToDelete.DeleteUser = entity.DeleteUser;  // Asignar el usuario que está eliminando la categoría

                // Puedes optar por eliminar físicamente la entidad de la base de datos si no se necesita mantener el registro
                // _shopContext.Categories.Remove(categoryToDelete);

                // Guardar los cambios en la base de datos
                _shopContext.SaveChanges();
            }
            else
            {
                // Manejar la situación donde la categoría no se encontró
                throw new Exception($"El producto con el ID {entity.ProductId} no fue encontrado.");
            }
        }

        public void SaveEntity(ProductsAddModel entity)
        {
            // Crear una nueva instancia de Categories a partir de CategoriesAddModel
            var ProductToAdd = new Products
            {
                ProductId = entity.ProductId,
                ProductName = entity.ProductName,
                SupplierId = entity.SupplierId,
                CategoryId = entity.CategoryId,
                UnitPrice = entity.UnitPrice,
                Discontinued = entity.Discontinued,

                Deleted = false  // Asumiendo que por defecto una categoría nueva no está eliminada
            };

            // Agregar la nueva categoría al contexto
            _shopContext.Products.Add(ProductToAdd);

            // Guardar cambios en la base de datos
            _shopContext.SaveChanges();
        }

        public void UpdateEntity(ProductsUpdateModel entity)
        {
            // Obtener la categoría a actualizar desde la base de datos
            var ProductToUpdate = _shopContext.Products.FirstOrDefault(x => x.ProductId == entity.ProductId);

            if (ProductToUpdate != null)
            {
                // Actualizar las propiedades de la categoría con los valores de CategoriesUpdateModel
                ProductToUpdate.ProductId = entity.ProductId;
                ProductToUpdate.ProductName = entity.ProductName;
                ProductToUpdate.SupplierId = entity.SupplierId;
                ProductToUpdate.CategoryId = entity.CategoryId;
                ProductToUpdate.UnitPrice = entity.UnitPrice;
                ProductToUpdate.Discontinued = entity.Discontinued;

                // Actualizar la categoría en el contexto
                _shopContext.Products.Update(ProductToUpdate);

                // Guardar cambios en la base de datos
                _shopContext.SaveChanges();
            }
            else
            {
                // Manejar la situación donde la categoría no se encontró
                throw new Exception($"El producto con el ID {entity.ProductId} no fue encontrado.");
            }
        }
    }

}
