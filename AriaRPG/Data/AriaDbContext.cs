using Microsoft.EntityFrameworkCore;
using AriaRPG.Models;

namespace AriaRPG.Data;

public class AriaDbContext : DbContext
{
    public AriaDbContext(DbContextOptions<AriaDbContext> options) : base(options) { }

    public DbSet<Player> Players => Set<Player>();
    public DbSet<Character> Characters => Set<Character>();
    public DbSet<CharacterSkill> CharacterSkills => Set<CharacterSkill>();
    public DbSet<CharacterSpecialSkill> CharacterSpecialSkills => Set<CharacterSpecialSkill>();
    public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
    public DbSet<Weapon> Weapons => Set<Weapon>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>()
            .HasOne(p => p.Character)
            .WithOne(c => c.Player)
            .HasForeignKey<Character>(c => c.PlayerId);

        modelBuilder.Entity<Character>()
            .HasMany(c => c.Skills)
            .WithOne(s => s.Character)
            .HasForeignKey(s => s.CharacterId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Character>()
            .HasMany(c => c.SpecialSkills)
            .WithOne(s => s.Character)
            .HasForeignKey(s => s.CharacterId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Character>()
            .HasMany(c => c.Inventory)
            .WithOne(i => i.Character)
            .HasForeignKey(i => i.CharacterId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Character>()
            .HasMany(c => c.Weapons)
            .WithOne(w => w.Character)
            .HasForeignKey(w => w.CharacterId)
            .OnDelete(DeleteBehavior.Cascade);

        // Seed 4 players
        modelBuilder.Entity<Player>().HasData(
            new Player { Id = 1, Name = "Joueur 1", Role = PlayerRole.Joueur },
            new Player { Id = 2, Name = "Joueur 2", Role = PlayerRole.Joueur },
            new Player { Id = 3, Name = "Joueur 3", Role = PlayerRole.Joueur },
            new Player { Id = 4, Name = "Maitre de Jeu", Role = PlayerRole.MaitreDeJeu }
        );
    }
}