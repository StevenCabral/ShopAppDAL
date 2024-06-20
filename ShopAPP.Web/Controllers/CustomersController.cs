using Microsoft.AspNetCore.Mvc;
using ShopAPP.DAL.Interfaces;
using ShopAPP.DAL.Models.Customers;

namespace ShopAPP.Web.Controllers
{
	public class CustomersController : Controller
	{
		private readonly IDbRepository<CustomersModel, CustomersAddModel, CustomersUpdateModel, CustomersRemoveModel> _customersDb;
		public CustomersController(IDbRepository<CustomersModel, CustomersAddModel, CustomersUpdateModel, CustomersRemoveModel> customers)
		{
			_customersDb = customers;
		}

		// GET: CustomersController
		public ActionResult Index()
		{
			return View(_customersDb.GetEntities());
		}

		// GET: CustomersController/Details/5
		public ActionResult Details(int id)
		{
			return View(_customersDb.GetEntityById(id));
		}

		// GET: CustomersController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: CustomersController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(CustomersAddModel collection)
		{
			try
			{
				collection.CreationDate = DateTime.Now;
				collection.CreationUser = 1;
				_customersDb.SaveEntity(collection);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: CustomersController/Edit/5
		public ActionResult Edit(int id)
		{
			var category = _customersDb.GetEntityById(id);
			var updatemodel = new CustomersUpdateModel()
			{
				CustId = category.CustId,
				Address = category.Address,
				City = category.City,
				CompanyName = category.CompanyName,
				Country = category.Country,
				ContactName = category.ContactName,
				PostalCode = category.PostalCode,
				Phone = category.Phone,
				ContactTitle = category.ContactTitle,
				Email = category.Email,
				Fax = category.Fax,
				Region = category.Region
			};
			return View(updatemodel);
		}

		// POST: CustomersController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, CustomersUpdateModel collection)
		{
			try
			{
				collection.CustId = 1;
				collection.ModifyUser = 1;
				collection.ModifyDate = DateTime.Now;

				_customersDb.UpdateEntity(collection);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

        // GET: CustomersController/Delete/5
        public ActionResult Delete(int id)
        {

            var removelement = new CustomersRemoveModel() { CustId = id };
            _customersDb.RemoveEntity(removelement);

            return RedirectToAction(nameof(Index));
        }

      
	}
}
