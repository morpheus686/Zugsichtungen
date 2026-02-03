using System.Data;
using System.Data.Common;
using Zugsichtungen.Abstractions.Interfaces;
using Zugsichtungen.Domain.Models.Gallery;
using Zugsichtungen.Domain.Models.Sighting;

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
        protected abstract string GetPictureDataQuery { get; }
        protected abstract string GetThumbnailDataQuery { get; }
        protected abstract SightingPicture MapSightingPicture(IDataReader reader);
        protected abstract ThumbnailData MapThumbnailData(IDataReader reader);
        protected abstract DbConnection CreateConnection(string connectionstring);

        public async Task<SightingPicture?> GetImageBySightingIdAsync(int sightingId)
        {
            using (var connection = await GetOpenedConnectionAsync())
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
                        return MapSightingPicture(reader);
                    }
                }
            }

            return null;
        }

        public async Task<bool> CheckIfImageExistsAsync(int sightingId)
        {
            using var connection = await GetOpenedConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = ExistsQuery;
            var param = command.CreateParameter();
            param.ParameterName = "@Id";
            param.Value = sightingId;
            command.Parameters.Add(param);

            var value = await command.ExecuteScalarAsync();
            return value != null && Convert.ToInt32(value) != 0;
        }

        public Task<Picture?> GetGalleryPictureByIdAsync(int imageId)
        {
            throw new NotImplementedException();
        }

        private async Task<DbConnection> GetOpenedConnectionAsync()
        {
            var connection = CreateConnection(this.connectionstring);
            await connection.OpenAsync();
            return connection;
        }

        public async Task<ThumbnailData?> GetThumbnailByIdAsync(int imageId)
        {
            using var connection = await GetOpenedConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = GetThumbnailDataQuery;
            var param = command.CreateParameter();
            param.ParameterName = "@Id";
            param.Value = imageId;
            command.Parameters.Add(param);

            using (var reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    return MapThumbnailData(reader);
                }
            }

            return null;
        }
    }
}
