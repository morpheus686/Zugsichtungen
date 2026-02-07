using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Zugsichtungen.Domain.Models.Gallery;
using Zugsichtungen.Domain.Models.Sighting;
using Zugsichtungen.Infrastructure.Repositories;

namespace Zugsichtungen.Infrastructure.SQLServer.Repositories
{
    public class SQLServerImageRepository : ImageRepositoryBase
    {
        public SQLServerImageRepository(string connectionString) : base(connectionString)
        {

        }

        protected override string ExistsQuery => "SELECT CASE WHEN EXISTS (SELECT 1 FROM SightingPicture WHERE SightingId = @Id) THEN 1 ELSE 0 END AS HatEintrag;";

        protected override string GetImageQuery => "SELECT Id, SightingId, Image, FileName FROM SightingPicture WHERE SightingId = @Id";

        protected override string GetPictureDataQuery => "SELECT Id, [Image] from SightingPicture WHERE Id = @Id;";

        protected override string GetThumbnailDataQuery => "SELECT Id, Thumbnail FROM SightingPicture WHERE Id = @Id;";

        protected override DbConnection CreateConnection(string connectionstring)
        {
            return new SqlConnection(connectionstring);
        }

        protected override PictureData MapPictureData(IDataReader reader)
        {
            var pictureData = reader["Image"];

            if (pictureData == DBNull.Value)
            {
                return PictureData.Create(reader.GetInt32(0), null);
            }

            return PictureData.Create(reader.GetInt32(0), (byte[]?)pictureData);
        }

        protected override SightingPicture MapSightingPicture(IDataReader reader)
        {
            return SightingPicture.Create(reader.GetInt32(0), reader.GetInt32(1), (byte[])reader["Image"], null, reader.GetString(3));
        }

        protected override ThumbnailData MapThumbnailData(IDataReader reader)
        {
            var thumbnailData = reader["Thumbnail"];

            if (thumbnailData == DBNull.Value)
            {
                return ThumbnailData.Create(reader.GetInt32(0), null);
            }

            return ThumbnailData.Create(reader.GetInt32(0), (byte[]?)thumbnailData);
        }
    }
}
