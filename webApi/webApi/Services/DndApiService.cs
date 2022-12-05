using webApi.Api;
using webApi.Api.DataClasses;
using webApi.Database;

namespace webApi.Services;

public class DndApiService : ApiService, IApiService
{
    private readonly ILiteDBOper _ldbBase;

    public DndApiService(ILiteDBOper ldbBase) : base(ldbBase)
    {
        _ldbBase = ldbBase;

        if (_ldbBase.GetLastUpdate().AddDays(1) < DateTime.Now)
        {
            try
            {
                
            }
        }
    }

    public async Task<SpellsList?> GetAllSpells()
    {
        SpellsList? allSpells;

        try
        {
            allSpells = await DndApi.GetAllSpells();
        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.ToString());
            return null;
        }

        return allSpells;
    }

    public async Task<SpellLong?> GetSpell(string index)
    {
        SpellLong? spell;
        try
        {
            spell = await DndApi.GetSpell(index);
        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.ToString());
            return null;
        }

        return spell;
    }
}
