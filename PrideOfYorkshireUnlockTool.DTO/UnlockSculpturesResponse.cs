using System.Text.Json.Serialization;

namespace PrideOfYorkshireUnlockTool.DTO
{
    public class UnlockSculpturesResponse
    {
        [JsonPropertyName("success")]
        public required bool Success { get; set; }
        [JsonPropertyName("accepted")]
        public required List<string> AcceptedIDs { get; set; }
    }
}