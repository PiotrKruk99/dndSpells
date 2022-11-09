using Newtonsoft.Json;

namespace webApi.Api;

public static class DndApi
{
    private static string uri = @"https://www.dnd5eapi.co/api/";
    private static HttpClient apiClient = new HttpClient();

    private static async Task<T?> GetFromApiAsync<T>(string uriEnd)
    {
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(uri + uriEnd)
        };

        var response = await apiClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(body);
        }

        return default(T);
    }

    public static async Task<SpellsList?> GetAllSpells()
    {
        return await GetFromApiAsync<SpellsList>("spells");
    }

    public static async Task<SpellLong?> GetSpell(string index)
    {
        if (index.Length == 0)
            return null;
        else
            return await GetFromApiAsync<SpellLong>("spells/" + index);
    }
}
