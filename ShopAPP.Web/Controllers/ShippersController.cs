using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopAPP.DAL.Interfaces;

#region ModelsName
using TModel = ShopAPP.DAL.Models.Shippers.ShippersModel;
using TAddModel = ShopAPP.DAL.Models.Shippers.ShippersAddModel;
using TUpdateModel = ShopAPP.DAL.Models.Shippers.ShippersUpdateModel;
using TRemoveModel = ShopAPP.DAL.Models.Shippers.ShippersRemoveModel;
using ShopAPP.DAL.Daos;
#endregion


namespace ShopAPP.Web.Controllers
{
    public class ShippersController : Controller
    {
        private readonly IDbRepository<TModel, TAddModel, TUpdateModel, TRemoveModel> _shippersDb;
        public ShippersController(IDbRepository<TModel, TAddModel, TUpdateModel, TRemoveModel> shippers)
        {
            _shippersDb = shippers;
        }

        // GET: EmployeesController
        public ActionResult Index()
        {
            return View(_shippersDb.GetEntities());
        }

        // GET: EmployeesController/Details/5
        public ActionResult Details(int id)
        {
            return View(_shippersDb.GetEntityById(id));
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
                _shippersDb.SaveEntity(collection);
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
            var employee = _shippersDb.GetEntityById(id);
            var updatemodel = new TUpdateModel()
            {
                ShipperId = employee.ShipperId,
                Address = employee.Address,
                City = employee.City,
                Country = employee.Country,
                PostalCode = employee.PostalCode,
                Phone = employee.Phone,
                Region = employee.Region,
                Name = employee.Name,
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
                collection.ShipperId = id;
                collection.ModifyUser = 1;
                collection.ModifyDate = DateTime.Now;
                _shippersDb.UpdateEntity(collection);
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

            var removelement = new TRemoveModel() { ShipperId = id };
            _shippersDb.RemoveEntity(removelement);

            return RedirectToAction(nameof(Index));
        }

        
    }
}
