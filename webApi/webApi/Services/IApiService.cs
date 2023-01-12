using webApi.Api.DataClassesDto;

namespace webApi.Services;

public interface IApiService
{
    public Task<IEnumerable<SpellShortDto>?> GetAllSpells();
    public Task<SpellLongDto?> GetSpell(string index);
}