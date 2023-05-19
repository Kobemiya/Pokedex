namespace PokedexBackend.Controllers.ResponseModels;

public class PokemonResponse : IResponseModel<PokemonResponse, Dbo.Pokemon>
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public int Def { get; set; }

    public int DefSpe { get; set; }

    public int Attack { get; set; }

    public int AttackSpe { get; set; }

    public int Speed { get; set; }

    public int Hp { get; set; }

    public string Type1 { get; set; } = null!;

    public string? Type2 { get; set; }

    public string Description { get; set; } = null!;

    public string? ImagePath { get; set; }

    public virtual ICollection<long> Attacks { get; set; } = new List<long>();

    public static PokemonResponse fromDbo(Dbo.Pokemon pokemon)
    {
        var result = new PokemonResponse();
        result.Id = pokemon.Id;
        result.Name = pokemon.Name;
        result.Type1 = pokemon.Type1;
        result.Type2 = pokemon.Type2;
        result.Description = pokemon.Description;
        result.Hp = pokemon.Hp;
        result.Attack = pokemon.Attack;
        result.AttackSpe = pokemon.AttackSpe;
        result.Def = pokemon.Def;
        result.DefSpe = pokemon.DefSpe;
        result.Speed = pokemon.Speed;
        result.ImagePath = pokemon.ImagePath;
        result.Attacks = pokemon.Attacks.Select(a => a.Id).ToList();
        return result;
    }
}
