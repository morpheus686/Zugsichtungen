using Microsoft.Data.Sqlite;
using System.Data;
using System.Data.Common;
using Zugsichtungen.Domain.Models;
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

        protected override SightingPicture MapReader(IDataReader reader)
        {
            return SightingPicture.Create(reader.GetInt32(0), reader.GetInt32(1), (byte[])reader["Bild"], null, reader.GetString(3));
        }
    }
}
