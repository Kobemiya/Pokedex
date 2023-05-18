using System.Numerics;

namespace WebApp.DataAccess.Dbo;

public interface IObjectWithId
{
    public long Id { get; set; }
}