using System.ComponentModel.DataAnnotations;

namespace AriaRPG.Models;

public enum MapType
{
    World,
    Local
}

public class GameMap
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public MapType Type { get; set; } = MapType.Local;

    /// <summary>Base64-encoded image data (data:image/...;base64,...)</summary>
    public string ImageData { get; set; } = string.Empty;

    /// <summary>Only one local map can be active at a time</summary>
    public bool IsActive { get; set; } = false;
}
