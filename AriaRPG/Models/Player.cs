namespace AriaRPG.Models;

public enum PlayerRole
{
    Joueur,
    MaitreDeJeu
}

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public PlayerRole Role { get; set; } = PlayerRole.Joueur;
    public Character? Character { get; set; }
}
