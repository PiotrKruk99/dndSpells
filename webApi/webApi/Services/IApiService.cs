using webApi.Api;

namespace webApi.Services;

public interface IApiService
{
    public Task<SpellsList?> GetAllSpells();
}