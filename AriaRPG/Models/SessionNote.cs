namespace AriaRPG.Models;

public class SessionNote
{
    public int Id { get; set; }

    /// <summary>Titre de la session, ex: "Session 3 - La Forêt Maudite"</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>Contenu de la note (texte libre, markdown-friendly)</summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>Date de la session de jeu</summary>
    public DateTime SessionDate { get; set; } = DateTime.Today;

    /// <summary>Dernière modification</summary>
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
