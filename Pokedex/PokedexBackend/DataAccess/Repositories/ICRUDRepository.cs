namespace PokedexBackend.DataAccess.Repositories;

public interface ICRUDRepository<DBEntity, ModelEntity>
{
    Task<ModelEntity?> GetById(long id, string includeTables = "");
    Task<IEnumerable<ModelEntity>> GetAll(string includeTables = "");
    Task<ModelEntity> Insert(ModelEntity entity);
    Task<ModelEntity> Update(ModelEntity entity);
    Task<bool> Delete(long idEntity);
}