using System.Data;
using System.Data.Common;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Interfaces;

namespace Zugsichtungen.Infrastructure.Repositories
{
    public abstract class ImageRepositoryBase : IImageRepository
    {
        private readonly string connectionstring;

        protected ImageRepositoryBase(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }

        protected abstract string ExistsQuery { get; }
        protected abstract string GetImageQuery { get; }
        protected abstract SightingPictureDto MapReader(IDataReader reader);
        protected abstract DbConnection CreateConnection(string connectionstring);

        public async Task<SightingPictureDto?> GetImageBySightingIdAsync(int sightingId)
        {
            using (var connection = await GetOpenedConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = GetImageQuery;
                var param = command.CreateParameter();
                param.ParameterName = "@Id";
                param.Value = sightingId;
                command.Parameters.Add(param);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return MapReader(reader);
                    }
                }
            }

            return null;
        }

        public async Task<bool> CheckIfImageExistsAsync(int sightingId)
        {
            using (var connection = await GetOpenedConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = ExistsQuery;
                var param = command.CreateParameter();
                param.ParameterName = "@Id";
                param.Value = sightingId;
                command.Parameters.Add(param);

                var value = await command.ExecuteScalarAsync();
                return value != null && Convert.ToInt32(value) != 0;
            }
        }

        private async Task<DbConnection> GetOpenedConnection()
        {
            var connection = CreateConnection(this.connectionstring);
            await connection.OpenAsync();
            return connection;
        }
    }
}
