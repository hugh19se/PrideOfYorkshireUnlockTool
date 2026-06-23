using System.Text.Json.Serialization;

namespace PrideOfYorkshireUnlockTool.DTO
{
    public class LoginAttemptRequest
    {
        [JsonPropertyName("email")]
        public required string EmailAddress { get; set; }
        [JsonPropertyName("password")]
        public required string Password { get; set; }
        [JsonPropertyName("device_name")]
        public string DeviceName = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/146.0.0.0 Safari/537.36";
    }
}