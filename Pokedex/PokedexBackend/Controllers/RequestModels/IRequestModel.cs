namespace PokedexBackend.Controllers.RequestModels;

public interface IRequestModel<TSelf, DboModel>
        where TSelf : IRequestModel<TSelf, DboModel>, new()
        where DboModel : Dbo.IObjectWithId, new()
{
    DboModel toDbo(long id = 0);
}
