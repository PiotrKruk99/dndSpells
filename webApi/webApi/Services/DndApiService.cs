using webApi.Api;
using webApi.Database;

namespace webApi.Services;

public class DndApiService : ApiService, IApiService
{
    private readonly ILiteDBOper _ldbBase;

    public DndApiService(ILiteDBOper ldbBase) : base(ldbBase)
    {
        _ldbBase = ldbBase;
    }

    public async Task<SpellsList?> GetAllSpells()
    {
        var allSpells = await DndApi.GetAllSpells();
        return allSpells;
    }

    public async Task<SpellLong?> GetSpell(string index)
    {
        var spell = await DndApi.GetSpell(index);
        return spell;
    }
}
