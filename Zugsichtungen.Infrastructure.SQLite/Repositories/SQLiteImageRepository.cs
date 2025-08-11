using Microsoft.Data.Sqlite;
using System.Data;
using System.Data.Common;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Infrastructure.Repositories;

namespace Zugsichtungen.Infrastructure.SQLite.Repositories
{
    public class SQLiteImageRepository : ImageRepositoryBase
    {
        public SQLiteImageRepository(string connectionString) : base(connectionString)
        {
        }

        protected override string ExistsQuery => "SELECT EXISTS (SELECT 1 FROM SichtungBild WHERE SichtungId = @Id) AS isExisting";

        protected override string GetImageQuery => "SELECT Id, SichtungId, Bild, Dateiname FROM SichtungBild WHERE SichtungId = @Id";

        protected override DbConnection CreateConnection(string connectionstring)
        {
            return new SqliteConnection(connectionstring);
        }

        protected override SightingPictureDto MapReader(IDataReader reader)
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
