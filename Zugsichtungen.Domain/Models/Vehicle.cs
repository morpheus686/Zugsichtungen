namespace Zugsichtungen.Domain.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        public string? Number { get; set; }

        public int? SeriesId { get; set; }

        public string? Comment { get; set; }

        private Vehicle()
        {
        }

        public static Vehicle Create(int id, string? number, int? seriesId, string? comment)
        {
            return new Vehicle()
            {
                Id = id,
                Number = number,
                SeriesId = seriesId,
                Comment = comment
            };
        }
    }
}
