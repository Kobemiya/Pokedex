namespace PokedexBackend.DataAccess.EfModels;

public partial class Pokemon : IModelWithId
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
}
