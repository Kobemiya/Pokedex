using System.Globalization;
using AutoMapper;
using WebApp.DataAccess.Dbo;
using WebApp.DataAccess.EfModels;
using WebApp.DataAccess.Interfaces;

namespace WebApp.DataAccess;

public class StatsRepository : Repository<TStat, Stat>, IStatsRepository
{
    public StatsRepository(PokedexContext context, ILogger<StatsRepository> logger, IMapper mapper) : base(context, logger, mapper) { }
    
    public async Task<Stat> Insert(Stat entity)
    {
        TStat dbEntity = _mapper.Map<TStat>(entity);
        dbEntity.IdUrlNavigation = _context.TShortcuts.Find(entity.IdUrlNavigation?.Id ?? entity.IdUrl); // links the Shortcut object
        _set.Add(dbEntity);
        try
        {
            await _context.SaveChangesAsync();
            Stat newEntity = _mapper.Map<Stat>(dbEntity);
            return newEntity;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error on db: {e.Message}");
            return null;
        }
    }
}