using webApi.Api;
using webApi.Database;

namespace webApi.Services;

public class DndApiService : ApiService, IApiService
{
    private readonly LiteDBOper _ldbBase;

    public DndApiService(LiteDBOper ldbBase) : base(ldbBase)
    {
        _ldbBase = ldbBase;
    }

    public async Task<SpellsList?> GetAllSpells()
    {
        var allSpells = await DndApi.GetAllSpells();
        return allSpells;
    }
}