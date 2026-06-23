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
            Client = new()
            {
                BaseAddress = new("https://prideofyorkshire.org/wp-json/trail/v1/"),
                Timeout = TimeSpan.FromMinutes(30)
            };
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

        public async Task<IEnumerable<string>> GetSculptureIDs()
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("sync");
            responseMessage.EnsureSuccessStatusCode();

            Stream responseStream = await responseMessage.Content.ReadAsStreamAsync();
            SyncResponse response = await JsonSerializer.DeserializeAsync<SyncResponse>(responseStream) ?? throw new JsonException("Sync Response Is Not In Expected Format");

            return response.Sculptures.Select(x => x.ID);
        }

        public async Task<IEnumerable<string>> UnlockSculptures(IEnumerable<string> sculptureIDs)
        {
            List<Sculpture> sculptures = [];
            foreach (string sculptureId in sculptureIDs)
            {
                sculptures.Add(new(sculptureId));
            }
            UnlockSculpturesRequest payload = new() { Sculptures = sculptures };

            HttpResponseMessage responseMessage = await Client.PostAsJsonAsync("collections", payload);
            responseMessage.EnsureSuccessStatusCode();

            Stream responseStream = await responseMessage.Content.ReadAsStreamAsync();
            UnlockSculpturesResponse response = await JsonSerializer.DeserializeAsync<UnlockSculpturesResponse>(responseStream) ?? throw new JsonException("Unlock Sculptures Response Is Not In Expected Format");

            return response.AcceptedIDs;
        }
    }
}