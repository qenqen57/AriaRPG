using System.ComponentModel.DataAnnotations;

namespace AriaRPG.Models;

public class CharacterSpecialSkill
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
    public int Value { get; set; } = 0;
    public string Description { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;

    public int CharacterId { get; set; }
    public Character? Character { get; set; }
}
