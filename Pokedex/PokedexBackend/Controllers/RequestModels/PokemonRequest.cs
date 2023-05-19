namespace PokedexBackend.Controllers.RequestModels;

public class PokemonRequest : IRequestModel<PokemonRequest, Dbo.Pokemon>
{
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

    public Dbo.Pokemon toDbo(long id = -1)
    {
        var result = new Dbo.Pokemon();
        result.Id = id;
        result.Name = Name;
        result.Type1 = Type1;
        result.Type2 = Type2;
        result.Description = Description;
        result.Hp = Hp;
        result.Attack = Attack;
        result.AttackSpe = AttackSpe;
        result.Def = Def;
        result.DefSpe = DefSpe;
        result.Speed = Speed;
        result.ImagePath = ImagePath;
        return result;
    }
}
