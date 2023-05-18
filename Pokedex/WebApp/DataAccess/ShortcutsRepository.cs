using AutoMapper;
using WebApp.DataAccess.Dbo;
using WebApp.DataAccess.EfModels;
using WebApp.DataAccess.Interfaces;

namespace WebApp.DataAccess;

public class ShortcutsRepository : Repository<EfModels.TShortcut, Dbo.Shortcut>, IShortcutsRepository
{
    public ShortcutsRepository(PokedexContext context, ILogger<ShortcutsRepository> logger, IMapper mapper) : base(context, logger, mapper) { }
    public Shortcut? GetByHash(string hash)
    {
        return _mapper.Map<Shortcut?>(_context.TShortcuts.FirstOrDefault(s => s.Hash == hash));
    }

    public IEnumerable<Shortcut> GetBySessionId(string sessionId)
    {
        return _context.TShortcuts
            .Where(s => s.SessionId == sessionId)
            .Select(s => _mapper.Map<Shortcut>(s));
    }
}