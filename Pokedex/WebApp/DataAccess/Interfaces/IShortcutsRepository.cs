using WebApp.DataAccess.Dbo;
using WebApp.DataAccess.EfModels;

namespace WebApp.DataAccess.Interfaces;

public interface IShortcutsRepository : IRepository<TShortcut, Shortcut>
{
    public Shortcut? GetByHash(string hash);
    public IEnumerable<Shortcut> GetBySessionId(string sessionId);
}