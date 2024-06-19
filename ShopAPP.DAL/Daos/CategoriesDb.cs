using ShopAPP.DAL.Context;
using ShopAPP.DAL.Entities;
using ShopAPP.DAL.Interfaces;
using ShopAPP.DAL.Models.Categories;

namespace ShopAPP.DAL.Daos
{
    public class CategoriesDb : ICategoriesDb
    {
        private readonly ShopContext _shopContext;

        public CategoriesDb(ShopContext context)
        {
            _shopContext = context;
        }

        public List<CategoriesModel> GetCategories()
        {
            return _shopContext.Categories.Where(x => x.Deleted == false).
                 Select(cat => new CategoriesModel()
                 {
                     CategoryId = cat.CategoryId,
                     CategoryName = cat.CategoryName,
                     Description = cat.Description,
                     Deleted = cat.Deleted,
                     CreationDate = cat.CreationDate,
                     CreationUser = cat.CreationUser
                 }).ToList();
        }

        public CategoriesModel GetCategoriesById(int id)
        {
            return _shopContext.Categories.Where(x => x.CategoryId == id).
                Select(cat => new CategoriesModel()
                {
                    CategoryId = cat.CategoryId,
                    CategoryName = cat.CategoryName,
                    Description = cat.Description,
                    Deleted = cat.Deleted,
                    CreationDate = cat.CreationDate,
                    CreationUser = cat.CreationUser
                }).First();
        }

        public void RemoveCategories(CategoriesRemoveModel categoriesRem)
        {
            // Encontrar la categoría que se desea eliminar en la base de datos
            var categoryToDelete = _shopContext.Categories.FirstOrDefault(x => x.CategoryId == categoriesRem.CategoryId);

            if (categoryToDelete != null)
            {
                // Marcar la categoría como eliminada
                categoryToDelete.Deleted = true;
                categoryToDelete.DeleteDate = DateTime.UtcNow;
                categoryToDelete.DeleteUser = categoriesRem.DeleteUser;  // Asignar el usuario que está eliminando la categoría

                // Puedes optar por eliminar físicamente la entidad de la base de datos si no se necesita mantener el registro
                // _shopContext.Categories.Remove(categoryToDelete);

                // Guardar los cambios en la base de datos
                _shopContext.SaveChanges();
            }
            else
            {
                // Manejar la situación donde la categoría no se encontró
                throw new Exception($"Category with ID {categoriesRem.CategoryId} not found.");
            }
        }

        public void SaveCategories(CategoriesAddModel categoriesAdd)
        {
            // Crear una nueva instancia de Categories a partir de CategoriesAddModel
            var categoryToAdd = new Categories
            {
                CategoryName = categoriesAdd.CategoryName,
                Description = categoriesAdd.Description,
                CreationDate = DateTime.UtcNow,
                CreationUser = categoriesAdd.CreationUser,
                Deleted = false  // Asumiendo que por defecto una categoría nueva no está eliminada
            };

            // Agregar la nueva categoría al contexto
            _shopContext.Categories.Add(categoryToAdd);

            // Guardar cambios en la base de datos
            _shopContext.SaveChanges();
        }



        public void UpdateCategories(CategoriesUpdateModel categoriesMod)
        {
            // Obtener la categoría a actualizar desde la base de datos
            var categoryToUpdate = _shopContext.Categories.FirstOrDefault(x => x.CategoryId == categoriesMod.CategoryId);

            if (categoryToUpdate != null)
            {
                // Actualizar las propiedades de la categoría con los valores de CategoriesUpdateModel
                categoryToUpdate.CategoryName = categoriesMod.CategoryName;
                categoryToUpdate.Description = categoriesMod.Description;
                categoryToUpdate.ModifyDate = DateTime.UtcNow;
                categoryToUpdate.ModifyUser = categoriesMod.ModifyUser;

                // Actualizar la categoría en el contexto
                _shopContext.Categories.Update(categoryToUpdate);

                // Guardar cambios en la base de datos
                _shopContext.SaveChanges();
            }
            else
            {
                // Manejar la situación donde la categoría no se encontró
                throw new Exception($"Category with ID {categoriesMod.CategoryId} not found.");
            }
        }

    }
}
