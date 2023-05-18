using System;
using System.Collections.Generic;

namespace WebApp.DataAccess.EfModels;

public partial class StatByUrl
{
    public string Url { get; set; } = null!;

    public int? TimesUsed { get; set; }
}
