namespace Zugsichtungen.Domain.Models
{
    public class Series
    {
        private Series()
        {
        }

        public int Id { get; set; }

        public string? Number { get; set; }

        public string? Comment { get; set; }

        public int? ModelId { get; set; }

        public static Series Create(int id, string? number, string? comment, int? modelId)
        {
            return new Series()
            {
                Id = id,
                Number = number,
                Comment = comment,
                ModelId = modelId
            };
        }
    }
}
