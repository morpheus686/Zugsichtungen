namespace Zugsichtungen.Domain.Models.Gallery
{
    public class PictureData
    {
        private PictureData() { }

        public int Id { get; private set; }
        public byte[]? Data { get; private set; }

        public static PictureData Create(int id, byte[]? data)
        {
            return new PictureData
            {
                Id = id,
                Data = data
            };
        }
    }
}
