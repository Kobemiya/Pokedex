namespace WebApp.DataAccess.Dbo;

public class Pokemon : IObjectWithId
{
    public long Id { get; set; }
    public string Url  { get; set; }
    public string Hash { get; set; }
    public string SessionId { get; set; }
    public ICollection<Stat> Stats { get; } = new List<Stat>();
}