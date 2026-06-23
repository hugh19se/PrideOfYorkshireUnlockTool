using System.Text.Json.Serialization;

namespace PrideOfYorkshireUnlockTool.DTO
{
    public class SyncResponse
    {
        [JsonPropertyName("sculptures")]
        public required List<Scuplture> Sculptures { get; set; }
    }

    public class Scuplture
    {
        [JsonPropertyName("id")]
        public required string ID { get; set; }
    }
}