using webApi.Api.DataClasses;

namespace webApi.Services;

public interface IApiService
{
    public Task<SpellsList?> GetAllSpells();
    public SpellLong? GetSpell(string index);
}