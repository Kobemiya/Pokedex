namespace WebApp.DataAccess;

public interface IRepository<DBEntity, ModelEntity>
{
    Task<IEnumerable<ModelEntity>> Get(string includeTables = "");
    Task<ModelEntity> Insert(ModelEntity entity);
    Task<ModelEntity> Update(ModelEntity entity);
    Task<bool> Delete(long idEntity);
}