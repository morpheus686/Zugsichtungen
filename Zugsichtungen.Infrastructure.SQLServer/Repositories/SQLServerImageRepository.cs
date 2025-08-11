using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Zugsichtungen.Abstractions.DTO;
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

        protected override DbConnection CreateConnection(string connectionstring)
        {
            return new SqlConnection(connectionstring);
        }

        protected override SightingPictureDto MapReader(IDataReader reader)
        {
            return new SightingPictureDto
            {
                Id = reader.GetInt32(0),
                SightingId = reader.GetInt32(1),
                Image = (byte[])reader["Image"],
                Filename = reader.GetString(3)
            };
        }
    }
}
