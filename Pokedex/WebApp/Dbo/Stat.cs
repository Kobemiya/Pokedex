﻿namespace WebApp.DataAccess.Dbo;

public class Stat : IObjectWithId
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public long IdUrl { get; set; }
    public virtual Pokemon IdUrlNavigation { get; set; } = null!;
}