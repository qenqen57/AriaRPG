using System.ComponentModel.DataAnnotations;

namespace AriaRPG.Models;

public class InventoryItem
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le nom est requis")]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; } = 1;
    public double Weight { get; set; } = 0;
    public string Category { get; set; } = "Divers";

    public int CharacterId { get; set; }
    public Character? Character { get; set; }
}