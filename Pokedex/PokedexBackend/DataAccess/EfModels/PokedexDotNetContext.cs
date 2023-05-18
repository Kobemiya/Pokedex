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

    public virtual DbSet<AttacksPokemon> AttacksPokemons { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

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
        });

        modelBuilder.Entity<AttacksPokemon>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Attacks_Pokemons");

            entity.Property(e => e.AttackId).HasColumnName("attack_id");
            entity.Property(e => e.PokemonId).HasColumnName("pokemon_id");

            entity.HasOne(d => d.Attack).WithMany()
                .HasForeignKey(d => d.AttackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attacks_AttacksPokemons");

            entity.HasOne(d => d.Pokemon).WithMany()
                .HasForeignKey(d => d.PokemonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pokemons_AttacksPokemons");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.PokemonId).HasColumnName("pokemon_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Pokemon).WithMany()
                .HasForeignKey(d => d.PokemonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pokemons_Favorites");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Favorites");
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
