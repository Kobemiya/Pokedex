namespace PokedexBackend.DataAccess.EfModels;

public partial class Attack : IModelWithId
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public int Damage { get; set; }

    public string Description { get; set; } = null!;

    public int Accuracy { get; set; }
}
