using System.Net.Http.Json;
using System.Text.Json;
using PrideOfYorkshireUnlockTool.DTO;

namespace PrideOfYorkshireUnlockTool.ConsoleApp
{
    public class APIHandler
    {
        private readonly HttpClient Client;
        public APIHandler()
        {
            Client = new() {BaseAddress = new("https://prideofyorkshire.org/wp-json/trail/v1/")};
        }

        public async Task<KeyValuePair<int, string>> AttemptLogin(string emailAddress, string password)
        {
            LoginAttemptRequest payload = new()
            {
                EmailAddress = emailAddress,
                Password = password
            };

            HttpResponseMessage responseMessage = await Client.PostAsJsonAsync("auth/login", payload);
            responseMessage.EnsureSuccessStatusCode();

            Stream responseStream = await responseMessage.Content.ReadAsStreamAsync();
            LoginAttemptResponse response = await JsonSerializer.DeserializeAsync<LoginAttemptResponse>(responseStream) ?? throw new JsonException("Login Attempt Response Is Not In Expected Format");
            Client.DefaultRequestHeaders.Authorization = new("Bearer", response.AccessToken);

            return new KeyValuePair<int, string>(response.UserInfo.UserID, response.UserInfo.DisplayName);
        }
    }
}