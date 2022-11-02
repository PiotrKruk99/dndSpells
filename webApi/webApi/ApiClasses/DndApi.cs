using Newtonsoft.Json;

namespace webApi.Api;

public static class DndApi
{
    private static string uri = @"https://www.dnd5eapi.co/api/";
    private static HttpClient apiClient = new HttpClient();

    public static async Task<SpellsList?> GetAllSpells()
    {
        var request = new HttpRequestMessage();
        request.Method = HttpMethod.Get;
        request.RequestUri = new Uri(uri + "spells");

        var response = await apiClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SpellsList>(body);
        }

        return null;
    }
}