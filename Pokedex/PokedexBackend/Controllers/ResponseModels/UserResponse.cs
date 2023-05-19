namespace PokedexBackend.Controllers.ResponseModels;

public class UserResponse : IResponseModel<UserResponse, Dbo.User>
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<long> Pokemons { get; set; } = new List<long>();

    public static UserResponse fromDbo(Dbo.User user)
    {
        var result = new UserResponse();
        result.Id = user.Id;
        result.Username = user.Username;
        result.Password = user.Password;
        result.Pokemons = user.Pokemons.Select(p => p.Id).ToList();
        return result;
    }
}
