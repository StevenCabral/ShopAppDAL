using Microsoft.EntityFrameworkCore;
using ShopAPP.DAL.Context;
using ShopAPP.DAL.Daos;
using ShopAPP.DAL.Interfaces;
using ShopAPP.DAL.Models.Customers;
using ShopAPP.DAL.Models.Employees;
using ShopAPP.DAL.Models.Products;
using ShopAPP.DAL.Models.Shippers;
using ShopAPP.DAL.Models.Suppliers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Registrar el contexto
builder.Services.AddDbContext<ShopContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ShopContext")));

builder.Services.AddTransient<ICategoriesDb, CategoriesDb>();
builder.Services.AddTransient<IDbRepository<CustomersModel, CustomersAddModel, CustomersUpdateModel, CustomersRemoveModel>, CustomersDb>();
builder.Services.AddTransient<IDbRepository<EmployeesModel, EmployeesAddModel, EmployeesUpdateModel, EmployeesRemoveModel>, EmployeesDb>();

builder.Services.AddTransient<IDbRepository<ProductsModel, ProductsAddModel, ProductsUpdateModel, ProductsRemoveModel>, ProductsDb>();
builder.Services.AddTransient<IDbRepository<ShippersModel, ShippersAddModel, ShippersUpdateModel, ShippersRemoveModel>, ShipperDb>();
builder.Services.AddTransient<IDbRepository<SuppliersModel, SuppliersAddModel, SuppliersUpdateModel, SuppliersRemoveModel>, SuppliersDb>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
