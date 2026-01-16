namespace Zugsichtungen.Domain.Models.Gallery
{
    public class Picture
    {
        private Picture()
        {
        }

        public int Id { get; private set; }
        public byte[] ImageData { get; private set; } = null!;

        public static Picture Create(int id, byte[] imageData)
        {
            return new Picture() { Id = id, ImageData = imageData };
        }
    }
}
