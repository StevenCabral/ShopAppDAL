namespace ShopAPP.DAL.Interfaces
{
    public interface IDbRepository<TModel, TAddModel, TUpdateModel, TRemoveModel>
    {
        List<TModel> GetEntities();
        TModel GetEntityById(int id);
        void SaveEntity(TAddModel entity);
        void UpdateEntity(TUpdateModel entity);
        void RemoveEntity(TRemoveModel entity);
    }

}
