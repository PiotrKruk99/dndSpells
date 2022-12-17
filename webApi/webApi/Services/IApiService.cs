using webApi.Api.DataClasses;
using webApi.Api.DataClassesDto;

namespace webApi.Services;

public interface IApiService
{
    public Task<SpellsList?> GetAllSpells();
    public SpellLongDto? GetSpell(string index);
}