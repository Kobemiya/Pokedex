using System.Numerics;

namespace PokedexBackend.Dbo;

public interface IObjectWithId
{
    public long Id { get; set; }
}