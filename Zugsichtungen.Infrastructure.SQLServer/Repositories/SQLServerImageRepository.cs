using Microsoft.Data.SqlClient;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Interfaces;

namespace Zugsichtungen.Infrastructure.SQLServer.Repositories
{
    public class SQLServerImageRepository : IImageRepository
    {
        private readonly string connectionString;

        public SQLServerImageRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<SightingPictureDto?> GetByIdAsync(int id)
        {
            using (var connection = await GetOpenedConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Id, SightingId, Image, FileName FROM SightingPicture WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);

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
