namespace PokedexBackend.Controllers.RequestModels;

public class AttackRequest : IRequestModel<AttackRequest, Dbo.Attack>
{

    public string Name { get; set; } = null!;

    public int Damage { get; set; }

    public string Description { get; set; } = null!;

    public int Accuracy { get; set; }

    public Dbo.Attack toDbo(long id = -1)
    {
        var result = new Dbo.Attack();
        result.Id = id;
        result.Name = Name;
        result.Description = Description;
        result.Damage = Damage;
        result.Accuracy = Accuracy;
        return result;
    }
}
