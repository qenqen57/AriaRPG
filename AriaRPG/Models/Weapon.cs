using System.ComponentModel.DataAnnotations;

namespace AriaRPG.Models;

public class Weapon
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le nom est requis")]
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Damage { get; set; } = string.Empty;
    public int Portee { get; set; } = 0;
    public string Description { get; set; } = string.Empty;
    public bool IsEquipped { get; set; } = false;

    public int CharacterId { get; set; }
    public Character? Character { get; set; }
}