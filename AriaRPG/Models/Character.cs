using System.ComponentModel.DataAnnotations;

namespace AriaRPG.Models;

public class Character
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le nom est requis")]
    public string Name { get; set; } = string.Empty;

    public string Race { get; set; } = string.Empty;
    public string Classe { get; set; } = string.Empty;
    public int Niveau { get; set; } = 1;

    // Caracteristiques principales (FOR, DEX, END, INT, CHA)
    public int Force { get; set; } = 10;
    public int Dexterite { get; set; } = 10;
    public int Endurance { get; set; } = 10;
    public int Intelligence { get; set; } = 10;
    public int Charisme { get; set; } = 10;

    // Points de vie
    public int PointsDeVieMax { get; set; } = 20;
    public int PointsDeVieActuels { get; set; } = 20;

    // Autres stats
    public int PointsDeMana { get; set; } = 0;
    public int PointsDeManaMax { get; set; } = 0;
    public int Armure { get; set; } = 0;
    public int Initiative { get; set; } = 0;
    public string Notes { get; set; } = string.Empty;

    // Buff temporaire (géré par le MJ)
    public int BuffCompetences { get; set; } = 0;

    // Relations
    public int PlayerId { get; set; }
    public Player? Player { get; set; }
    public List<CharacterSkill> Skills { get; set; } = new();
    public List<CharacterSpecialSkill> SpecialSkills { get; set; } = new();
    public List<InventoryItem> Inventory { get; set; } = new();
    public List<Weapon> Weapons { get; set; } = new();
}