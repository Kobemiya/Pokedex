namespace PokedexBackend.Controllers.ResponseModels;

public class AttackResponse : IResponseModel<AttackResponse, Dbo.Attack>
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public int Damage { get; set; }

    public string Description { get; set; } = null!;

    public int Accuracy { get; set; }

    public static AttackResponse fromDbo(Dbo.Attack attack)
    {
        var result = new AttackResponse();
        result.Id = attack.Id;
        result.Name = attack.Name;
        result.Description = attack.Description;
        result.Damage = attack.Damage;
        result.Accuracy = attack.Accuracy;
        return result;
    }
}
