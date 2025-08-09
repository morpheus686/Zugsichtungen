using Microsoft.Data.Sqlite;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Interfaces;

namespace Zugsichtungen.Infrastructure.SQLite.Repositories
{
    public class SQLiteImageRepository : IImageRepository
    {
        private readonly string connectionString;

        public SQLiteImageRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<SightingPictureDto?> GetImageBySightingIdAsync(int sightingId)
        {
            using (var connection = await GetOpenedConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Id, SichtungId, Bild, Dateiname FROM SichtungBild WHERE SichtungId = @Id";
                    command.Parameters.AddWithValue("@Id", sightingId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new SightingPictureDto
                            {
                                Id = reader.GetInt32(0),
                                SightingId = reader.GetInt32(1),
                                Image = (byte[])reader["Bild"],
                                Filename = reader.GetString(3)
                            };
                        }
                    }
                }

                return null;
            }
        }

        private async Task<SqliteConnection> GetOpenedConnection()
        {
            var connection = new SqliteConnection(this.connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}
