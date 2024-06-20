using ShopAPP.DAL.Context;
using ShopAPP.DAL.Entities;
using ShopAPP.DAL.Interfaces;
using ShopAPP.DAL.Models.OrderDetails;

namespace ShopAPP.DAL.Daos
{

	public class OrderDetailsDb : IOrderDetailDb
	{
		private readonly ShopContext _shopContext;

		public OrderDetailsDb(ShopContext context)
		{
			_shopContext = context;
		}
		public List<OrderDetailsModel> GetOrderDetail(int orderId)
		{
			return _shopContext.OrderDetails.Where(x => x.OrderId == orderId).
				 Select(OrDet => new OrderDetailsModel()
				 {
					 OrderId = OrDet.OrderId,
					 ProductId = OrDet.ProductId,
					 UnitPrice = OrDet.UnitPrice,
					 Qty = OrDet.Qty,
					 Discount = OrDet.Discount,
				 }).ToList();
		}


		public void RemoveOrderDetail(OrderDetailsUpdateModel entity)
		{
			throw new NotImplementedException();
		}

		public void RemoveOrderDetail(OrderDetailsRemoveModel categoriesRem)
		{
			throw new NotImplementedException();
		}

		public void SaveOrderDetail(OrderDetailsAddModel entity)
		{
			// Crear una nueva instancia de Categories a partir de CategoriesAddModel
			var OrderDetailToAdd = new OrderDetails
			{

				OrderId = entity.OrderId,
				ProductId = entity.ProductId,
				UnitPrice = entity.UnitPrice,
				Qty = entity.Qty,
				Discount = entity.Discount,

			};

			// Agregar la nueva categoría al contexto
			_shopContext.OrderDetails.Add(OrderDetailToAdd);

			// Guardar cambios en la base de datos
			_shopContext.SaveChanges();
		}

		public void SaveOrderDetail(OrderDetailsModel entity)
		{
			throw new NotImplementedException();
		}


		public void UpdateOrderDetail(OrderDetailsUpdateModel entity)
		{
			// Obtener la categoría a actualizar desde la base de datos
			var OrderDetailToUpdate = _shopContext.OrderDetails.FirstOrDefault(x => x.OrderId == entity.OrderId);

			if (OrderDetailToUpdate != null)
			{
				// Actualizar las propiedades de la categoría con los valores de CategoriesUpdateModel
				OrderDetailToUpdate.OrderId = entity.OrderId;
				OrderDetailToUpdate.ProductId = entity.ProductId;
				OrderDetailToUpdate.UnitPrice = entity.UnitPrice;
				OrderDetailToUpdate.Qty = entity.Qty;
				OrderDetailToUpdate.Discount = entity.Discount;


				// Actualizar la categoría en el contexto
				_shopContext.OrderDetails.Update(OrderDetailToUpdate);

				// Guardar cambios en la base de datos
				_shopContext.SaveChanges();
			}
			else
			{
				// Manejar la situación donde la categoría no se encontró
				throw new Exception($"El detalle de la orden con ID {entity.OrderId} no fue encontrado.");
			}
		}

		public void UpdateOrderDetail(OrderDetailsRemoveModel entity)
		{
			throw new NotImplementedException();
		}


	}

}
