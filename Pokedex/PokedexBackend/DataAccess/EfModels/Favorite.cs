using System;
using System.Collections.Generic;

namespace PokedexBackend.DataAccess.EfModels;

public partial class Favorite
{
    public long PokemonId { get; set; }

    public long UserId { get; set; }

    public virtual Pokemon Pokemon { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
