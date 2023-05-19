namespace PokedexBackend.Controllers.ResponseModels;

public interface IResponseModel<TSelf, DboModel>
        where TSelf : IResponseModel<TSelf, DboModel>, new()
        where DboModel : Dbo.IObjectWithId, new()
{
    static abstract TSelf fromDbo(DboModel dbo);
}
