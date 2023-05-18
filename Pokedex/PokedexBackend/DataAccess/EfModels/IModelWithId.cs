using System.Numerics;

namespace PokedexBackend.DataAccess.EfModels;

public interface IModelWithId
{
    public long Id { get; set; }
}