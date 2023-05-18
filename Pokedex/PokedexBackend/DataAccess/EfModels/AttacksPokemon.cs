namespace PokedexBackend.DataAccess.EfModels;

public partial class AttacksPokemon
{
    public long PokemonId { get; set; }

    public long AttackId { get; set; }

    public virtual Attack Attack { get; set; } = null!;

    public virtual Pokemon Pokemon { get; set; } = null!;
}
