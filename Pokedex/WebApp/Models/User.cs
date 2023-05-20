using PokedexBackend.Dbo;

namespace WebApp.Pages
{
    public class User
    {
        public long Id { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public ICollection<long> Pokemons { get; set; } = new List<long>();
    }
}
