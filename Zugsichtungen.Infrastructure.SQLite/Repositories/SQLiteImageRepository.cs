using Microsoft.Data.Sqlite;
using System.Data;
using System.Data.Common;
using Zugsichtungen.Domain.Models.Gallery;
using Zugsichtungen.Domain.Models.Sighting;
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

        protected override string GetPictureDataQuery => throw new NotImplementedException();

        protected override string GetThumbnailDataQuery =>"SELECT Id, Thumbnail FROM SichtungBild WHERE SichtungBild.Id = @Id"; 

        protected override DbConnection CreateConnection(string connectionstring)
        {
            return new SqliteConnection(connectionstring);
        }

        protected override SightingPicture MapSightingPicture(IDataReader reader)
        {
            return SightingPicture.Create(reader.GetInt32(0), reader.GetInt32(1), (byte[])reader["Bild"], null, reader.GetString(3));
        }

        protected override ThumbnailData MapThumbnailData(IDataReader reader)
        {
            var thumbnailData = reader["Thumbnail"];

            if (thumbnailData == DBNull.Value)
            {
                return ThumbnailData.Create(reader.GetInt32(0), null);
            }

            return ThumbnailData.Create(reader.GetInt32(0), (byte[]?)reader["Thumbnail"]);
        }
    }
}
