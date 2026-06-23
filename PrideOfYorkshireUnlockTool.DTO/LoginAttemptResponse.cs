using System.Text.Json.Serialization;

namespace PrideOfYorkshireUnlockTool.DTO
{
    public class LoginAttemptResponse
    {
        [JsonPropertyName("access_token")]
        public required string AccessToken { get; set; }
        [JsonPropertyName("user")]
        public required UserInfo UserInfo { get; set; }
    }

    public class UserInfo
    {
        [JsonPropertyName("id")]
        public required int UserID { get; set; }
        [JsonPropertyName("display_name")]
        public required string DisplayName { get; set; }
    }
}