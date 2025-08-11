using Microsoft.Data.SqlClient;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Infrastructure.Repositories;

namespace Zugsichtungen.Infrastructure.SQLServer.Repositories
{
    public class SQLServerImageRepository : ImageRepositoryBase
    {
        private readonly string connectionString;

        public SQLServerImageRepository(string connectionString)// : base(connectionString)
        {
            this.connectionString = connectionString;
        }

        public override async Task<bool> CheckIfImageExistsAsync(int sightingId)
        {
            using (var connection = await GetOpenedConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT CASE WHEN EXISTS (SELECT 1 FROM SightingPicture WHERE SightingId = @Id) THEN 1 ELSE 0 END AS HatEintrag;";
                    command.Parameters.AddWithValue("@Id", sightingId);

                    var value = await command.ExecuteScalarAsync();

                    if (value == null)
                    {
                        return false;
                    }

                    var isExisting = (Int32)value;

                    if (isExisting == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }

        public override async Task<SightingPictureDto?> GetImageBySightingIdAsync(int sightingId)
        {
            using (var connection = await GetOpenedConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Id, SightingId, Image, FileName FROM SightingPicture WHERE SightingId = @Id";
                    command.Parameters.AddWithValue("@Id", sightingId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
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

                return null;
            }
        }

        private async Task<SqlConnection> GetOpenedConnection()
        {
            var connection = new SqlConnection(this.connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}
