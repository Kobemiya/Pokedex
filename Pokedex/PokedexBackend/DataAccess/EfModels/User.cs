﻿using System;
using System.Collections.Generic;

namespace PokedexBackend.DataAccess.EfModels;

public partial class User : IModelWithId
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Pokemon> Pokemons { get; set; } = new List<Pokemon>();
}
