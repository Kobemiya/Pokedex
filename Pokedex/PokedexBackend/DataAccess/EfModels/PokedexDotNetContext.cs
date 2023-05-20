using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PokedexBackend.DataAccess.EfModels;

public partial class PokedexDotNetContext : DbContext
{
    public PokedexDotNetContext()
    {
    }

    public PokedexDotNetContext(DbContextOptions<PokedexDotNetContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attack> Attacks { get; set; }

    public virtual DbSet<Pokemon> Pokemons { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Database=PokedexDotNet;Trusted_Connection=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attack>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Accuracy).HasColumnName("accuracy");
            entity.Property(e => e.Damage).HasColumnName("damage");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("type");
        });

        modelBuilder.Entity<Pokemon>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Attack).HasColumnName("attack");
            entity.Property(e => e.AttackSpe).HasColumnName("attack_spe");
            entity.Property(e => e.Def).HasColumnName("def");
            entity.Property(e => e.DefSpe).HasColumnName("def_spe");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Hp).HasColumnName("hp");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("image_path");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Speed).HasColumnName("speed");
            entity.Property(e => e.Type1)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("type_1");
            entity.Property(e => e.Type2)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("type_2");

            entity.HasMany(d => d.Attacks).WithMany(p => p.Pokemons)
                .UsingEntity<Dictionary<string, object>>(
                    "AttacksPokemon",
                    r => r.HasOne<Attack>().WithMany()
                        .HasForeignKey("AttackId")
                        .HasConstraintName("FK_Attacks_AttacksPokemons"),
                    l => l.HasOne<Pokemon>().WithMany()
                        .HasForeignKey("PokemonId")
                        .HasConstraintName("FK_Pokemons_AttacksPokemons"),
                    j =>
                    {
                        j.HasKey("PokemonId", "AttackId");
                        j.ToTable("Attacks_Pokemons");
                        j.IndexerProperty<long>("PokemonId").HasColumnName("pokemon_id");
                        j.IndexerProperty<long>("AttackId").HasColumnName("attack_id");
                    });

            entity.HasMany(d => d.Users).WithMany(p => p.Pokemons)
                .UsingEntity<Dictionary<string, object>>(
                    "Favorite",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Users_Favorites"),
                    l => l.HasOne<Pokemon>().WithMany()
                        .HasForeignKey("PokemonId")
                        .HasConstraintName("FK_Pokemons_Favorites"),
                    j =>
                    {
                        j.HasKey("PokemonId", "UserId");
                        j.ToTable("Favorites");
                        j.IndexerProperty<long>("PokemonId").HasColumnName("pokemon_id");
                        j.IndexerProperty<long>("UserId").HasColumnName("user_id");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Username, "IX_Users_Username").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
