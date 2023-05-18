using System.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using WebApp.DataAccess.EfModels;

namespace WebApp.DataAccess;

public class Repository<DBEntity, ModelEntity> : IRepository<DBEntity, ModelEntity>
where DBEntity : class, new()
where ModelEntity : class, Dbo.IObjectWithId, new()
{
    protected readonly DbSet<DBEntity> _set;
    protected readonly PokedexContext _context;
    protected readonly ILogger<Repository<DBEntity, ModelEntity>> _logger;
    protected readonly IMapper _mapper;
    
    public Repository(PokedexContext context, ILogger<Repository<DBEntity, ModelEntity>> logger, IMapper mapper)
    {
        _set = context.Set<DBEntity>();
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ModelEntity>> Get(string includeTables = "")
    {
        try
        {
            List<DBEntity> query;
            if (String.IsNullOrEmpty(includeTables))
                query = await _set.AsNoTracking().ToListAsync();
            else
                query = await _set.Include(includeTables).AsNoTracking().ToListAsync();

            return _mapper.Map<ModelEntity[]>(query);
        }
        catch (Exception e)
        {
            _logger.LogError($"Error on db: {e.Message}");
            return null;
        }
    }

    public async Task<ModelEntity> Insert(ModelEntity entity)
    {
        DBEntity dbEntity = _mapper.Map<DBEntity>(entity);
        _set.Add(dbEntity);
        try
        {
            await _context.SaveChangesAsync();
            ModelEntity newEntity = _mapper.Map<ModelEntity>(dbEntity);
            return newEntity;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error on db: {e.Message}");
            return null;
        }
    }

    public async Task<ModelEntity> Update(ModelEntity entity)
    {
        try
        {
            DBEntity dbEntity = _set.Find(entity.Id) ?? throw new DataException($"Entity of type '{typeof(ModelEntity).Name}' not found with id {entity.Id}");
        
            _mapper.Map(entity, dbEntity);
            if (!_context.ChangeTracker.HasChanges())
                return entity;

            await _context.SaveChangesAsync();
            return _mapper.Map<ModelEntity>(dbEntity);
        }
        catch (Exception e)
        {
            _logger.LogError($"Error on db: {e.Message}");
            return null;
        }
    }

    public async Task<bool> Delete(long idEntity)
    {
        try
        {
            DBEntity dbEntity = _set.Find(idEntity) ?? throw new DataException($"Entity of type '{typeof(ModelEntity).Name}' not found with id {idEntity}");
            _set.Remove(dbEntity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error on db: {e.Message}");
            return false;
        }
    }
}