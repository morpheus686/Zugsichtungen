using System.Text.Json.Serialization;

namespace Zugsichtungen.Webclients
{
    public abstract class ODataServiceBase
    {
        protected class ODataResponse<T>
        {
            [JsonPropertyName("value")]
            public List<T> Value { get; set; } = new();
        }
    }
}
