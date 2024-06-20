using Microsoft.AspNetCore.Mvc;
using ShopAPP.DAL.Interfaces;
using ShopAPP.DAL.Models.Categories;

namespace ShopAPP.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesDb _categoriesDb;
        public CategoriesController(ICategoriesDb categories)
        {
            _categoriesDb = categories;
        }

        // GET: CategoriesController
        public ActionResult Index()
        {
            var categories = _categoriesDb.GetCategories();

            return View(categories);
        }

        // GET: CategoriesController/Details/5
        public ActionResult Details(int id)
        {
            return View(_categoriesDb.GetCategoriesById(id));
        }

        // GET: CategoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoriesAddModel collection)
        {
            try
            {
                collection.CreationDate = DateTime.Now;
                collection.CreationUser = 1;
                _categoriesDb.SaveCategories(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriesController/Edit/5
        public ActionResult Edit(int id)
        {
            var category = _categoriesDb.GetCategoriesById(id);
            var updatemodel = new CategoriesUpdateModel()
            {
                CategoryId = id,
                CategoryName = category.CategoryName,
                Description = category.Description
            };
            return View(updatemodel);
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoriesUpdateModel collection)
        {
            try
            {
                collection.CategoryId = id;
                collection.ModifyDate = DateTime.Now;
                collection.ModifyUser = 1;
                _categoriesDb.UpdateCategories(collection);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriesController/Delete/5
        public ActionResult Delete(int id)
        {
           
                var removelement = new CategoriesRemoveModel() { CategoryId = id };
                _categoriesDb.RemoveCategories(removelement);

                return RedirectToAction(nameof(Index));
        }

       
    }
}
