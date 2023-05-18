using System;
using System.Collections.Generic;

namespace WebApp.DataAccess.EfModels;

public partial class TStat
{
    public long Id { get; set; }

    public long IdUrl { get; set; }

    public DateTime Date { get; set; }

    public virtual TShortcut IdUrlNavigation { get; set; } = null!;
}
