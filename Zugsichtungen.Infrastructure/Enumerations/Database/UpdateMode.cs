namespace Zugsichtungen.Infrastructure.Enumerations.Database
{
    public enum UpdateMode
    {
        Full,       // Überschreibt alle Felder
        Partial,    // Überschreibt nur angegebene Felder
        Tracked     // Lädt das Entity zuerst
    }
}
