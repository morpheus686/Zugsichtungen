namespace Zugsichtungen.Infrastructure.SQLite.Helpers
{
    public static class SqliteHelper
    {
        public static string CopyDatabaseIfNotExits()
        {
            var appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Zugsichtungen");

            Directory.CreateDirectory(appDataPath);

            var dbPath = Path.Combine(appDataPath, "zugbeobachtungen.db");

            // Falls die Datei noch nicht existiert → aus dem Projektverzeichnis (z.B. Infrastructure.Sqlite) kopieren
            if (!File.Exists(dbPath))
            {
                // Pfad zur mitgelieferten DB in deinem Projekt
                var sourcePath = Path.Combine(AppContext.BaseDirectory, "zugbeobachtungen.db");

                if (File.Exists(sourcePath))
                {
                    File.Copy(sourcePath, dbPath);
                }
            }

            return dbPath;
        }
    }
}
