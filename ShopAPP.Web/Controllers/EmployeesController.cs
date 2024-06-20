using Microsoft.AspNetCore.Mvc;
using ShopAPP.DAL.Interfaces;

#region ModelsName
using TModel = ShopAPP.DAL.Models.Employees.EmployeesModel;
using TAddModel = ShopAPP.DAL.Models.Employees.EmployeesAddModel;
using TUpdateModel = ShopAPP.DAL.Models.Employees.EmployeesUpdateModel;
using TRemoveModel = ShopAPP.DAL.Models.Employees.EmployeesRemoveModel;
#endregion

namespace ShopAPP.Web.Controllers
{
	public class EmployeesController : Controller
	{
		private readonly IDbRepository<TModel, TAddModel, TUpdateModel, TRemoveModel> _employeesDb;
		public EmployeesController(IDbRepository<TModel, TAddModel, TUpdateModel, TRemoveModel> employees)
		{
			_employeesDb = employees;
		}

		// GET: EmployeesController
		public ActionResult Index()
		{
			return View(_employeesDb.GetEntities());
		}

		// GET: EmployeesController/Details/5
		public ActionResult Details(int id)
		{
			return View(_employeesDb.GetEntityById(id));
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
				_employeesDb.SaveEntity(collection);
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
			var employee = _employeesDb.GetEntityById(id);
			var updatemodel = new TUpdateModel()
			{
				EmpId = employee.EmpId,
				Address = employee.Address,
				City = employee.City,
				Country = employee.Country,
				PostalCode = employee.PostalCode,
				Phone = employee.Phone,
				Region = employee.Region,
				BirthDate = employee.BirthDate,
				FirstName = employee.FirstName,
				HireDate = employee.HireDate,
				LastName = employee.LastName,
				MgrId = employee.MgrId,
				Title = employee.Title,
				TitleOfCourtesy = employee.TitleOfCourtesy
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
				collection.EmpId = id;	
				collection.ModifyUser = 1;
				collection.ModifyDate = DateTime.Now;
				_employeesDb.UpdateEntity(collection);
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
