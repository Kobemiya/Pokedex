namespace PokedexBackend.Controllers.RequestModels;

public class UserRequest : IRequestModel<UserRequest, Dbo.User>
{

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Dbo.User toDbo(long id = 0)
    {
        var result = new Dbo.User();
        result.Id = id;
        result.Username = Username;
        result.Password = Password;
        return result;
    }
}
