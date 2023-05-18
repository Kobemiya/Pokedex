using System;
using System.Collections.Generic;

namespace WebApp.DataAccess.EfModels;

public partial class TPokemons
{
    public long Id { get; set; }

    public string Url { get; set; } = null!;

    public string Hash { get; set; } = null!;

    public string SessionId { get; set; } = null!;

    public virtual ICollection<TStat> TStats { get; } = new List<TStat>();
}
