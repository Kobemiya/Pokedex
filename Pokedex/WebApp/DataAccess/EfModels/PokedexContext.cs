using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApp.DataAccess.EfModels;

public partial class PokedexContext : DbContext
{
    public PokedexContext()
    {
    }

    public PokedexContext(DbContextOptions<PokedexContext> options)
        : base(options)
    {
    }

    public virtual DbSet<StatByUrl> StatByUrls { get; set; }

    public virtual DbSet<TPokemons> TShortcuts { get; set; }

    public virtual DbSet<TStat> TStats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=SmartLink;Trusted_Connection=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StatByUrl>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("StatByUrl");

            entity.Property(e => e.TimesUsed).HasColumnName("times_used");
            entity.Property(e => e.Url)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("url");
        });

        modelBuilder.Entity<TPokemons>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_T_Shorcuts");

            entity.ToTable("T_Shortcuts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Hash)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("hash");
            entity.Property(e => e.SessionId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sessionId");
            entity.Property(e => e.Url)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("url");

            entity.HasMany(d => d.TStats).WithOne(p => p.IdUrlNavigation);
        });

        modelBuilder.Entity<TStat>(entity =>
        {
            entity.ToTable("T_Stats");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.IdUrl).HasColumnName("idUrl");

            entity.HasOne(d => d.IdUrlNavigation).WithMany(p => p.TStats)
                .HasForeignKey(d => d.IdUrl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Stats_T_Shorcuts");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
