using Microsoft.AspNetCore.Mvc;
using ShopAPP.DAL.Interfaces;

#region ModelsName
using TModel = ShopAPP.DAL.Models.Products.ProductsModel;
using TAddModel = ShopAPP.DAL.Models.Products.ProductsAddModel;
using TUpdateModel = ShopAPP.DAL.Models.Products.ProductsUpdateModel;
using TRemoveModel = ShopAPP.DAL.Models.Products.ProductsRemoveModel;
#endregion

namespace ShopAPP.Web.Controllers
{
	public class ProductsController : Controller
	{
		private readonly IDbRepository<TModel, TAddModel, TUpdateModel, TRemoveModel> _productsDb;
		public ProductsController(IDbRepository<TModel, TAddModel, TUpdateModel, TRemoveModel> products)
		{
			_productsDb = products;
		}

		// GET: EmployeesController
		public ActionResult Index()
		{
			return View(_productsDb.GetEntities());
		}

		// GET: EmployeesController/Details/5
		public ActionResult Details(int id)
		{
			return View(_productsDb.GetEntityById(id));
		}

		// GET: EmployeesController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: EmployeesController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(TAddModel collection)
		{
			try
			{
				collection.CreationUser = 1;
				collection.CreationDate = DateTime.Now;
				_productsDb.SaveEntity(collection);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: EmployeesController/Edit/5
		public ActionResult Edit(int id)
		{
			var product = _productsDb.GetEntityById(id);
			var updatemodel = new TUpdateModel()
			{
				ProductId = product.ProductId,
				CategoryId = product.CategoryId,
				Discontinued = product.Discontinued,
				ProductName = product.ProductName,
				SupplierId = product.SupplierId,
				UnitPrice = product.UnitPrice,
			};
			return View(updatemodel);
		}

		// POST: EmployeesController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, TUpdateModel collection)
		{
			try
			{
				collection.ProductId = id;
				collection.ModifyUser = 1;
				collection.ModifyDate = DateTime.Now;
				_productsDb.UpdateEntity(collection);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: EmployeesController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: EmployeesController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
