namespace Zugsichtungen.Domain.Models.Gallery
{
    public class ThumbnailData
    {
        private ThumbnailData()
        {
        }

        public int Id { get; private set; }
        public byte[]? Data { get; private set; }

        public static ThumbnailData Create(int id, byte[]? data)
        {
            return new ThumbnailData
            {
                Id = id,
                Data = data
            };
        }
    }
}
