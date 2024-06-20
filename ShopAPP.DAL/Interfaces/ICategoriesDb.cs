using ShopAPP.DAL.Models.Categories;

namespace ShopAPP.DAL.Interfaces
{
	public interface ICategoriesDb
	{
		List<CategoriesModel> GetCategories();
		CategoriesModel GetCategoriesById(int id);
		void SaveCategories(CategoriesAddModel categoriesAdd);
		void UpdateCategories(CategoriesUpdateModel categoriesMod);
		void RemoveCategories(CategoriesRemoveModel categoriesRem);
	}
}
