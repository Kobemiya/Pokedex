namespace PokedexBackend.Controllers.RequestModels;

public class AttackRequest : IRequestModel<AttackRequest, Dbo.Attack>
{

    public string Name { get; set; } = null!;

    public int Damage { get; set; }

    public string Description { get; set; } = null!;

    public int Accuracy { get; set; }

    public string Type { get; set; } = null!;

    public Dbo.Attack toDbo(long id = 0)
    {
        var result = new Dbo.Attack();
        result.Id = id;
        result.Name = Name;
        result.Description = Description;
        result.Damage = Damage;
        result.Accuracy = Accuracy;
        result.Type = attack.Type;
        return result;
    }
}
