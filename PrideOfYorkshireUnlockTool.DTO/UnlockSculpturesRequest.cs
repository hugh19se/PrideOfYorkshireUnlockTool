using System.Text.Json.Serialization;

namespace PrideOfYorkshireUnlockTool.DTO
{
    public class UnlockSculpturesRequest
    {
        [JsonPropertyName("collections")]
        public required List<Sculpture> Sculptures { get; set; }
    }

    public class Sculpture(string sculptureId)
    {
        [JsonPropertyName("sculptureId")]
        public string SculptureID { get; init; } = sculptureId;
        [JsonPropertyName("collectedAt")]
        public long CollectedAt { get; } = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }
}