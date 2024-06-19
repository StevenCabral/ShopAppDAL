using ShopAPP.DAL.Models.OrderDetails;

namespace ShopAPP.DAL.Interfaces
{
    public interface IOrderDetailDb
    {
        List<OrderDetailsModel> GetOrderDetail(int id);
        void SaveOrderDetail(OrderDetailsAddModel categoriesAdd);
        void UpdateOrderDetail(OrderDetailsUpdateModel categoriesMod);
        void RemoveOrderDetail(OrderDetailsRemoveModel categoriesRem);
    }
}
