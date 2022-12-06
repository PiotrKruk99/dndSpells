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

        UpdateDatabase().Start();
    }

    private async Task<bool> UpdateDatabase()
    {
        if (_ldbBase.GetLastUpdate().AddDays(1) < DateTime.Now)
        {
            try
            {
                var lastUpdate = _ldbBase.GetLastUpdate();

                if (lastUpdate.AddDays(1) < DateTime.Now)
                {
                    var allSpells = await DndApi.GetAllSpells();

                    if (allSpells is null || allSpells.results is null)
                        return false;

                    List<SpellLong> spellsList = new List<SpellLong>();

                    foreach (var spellShort in allSpells.results)
                    {
                        if (spellShort.index is not null)
                        {
                            var spellLong = await DndApi.GetSpell(spellShort.index);

                            if (spellLong is null)
                            {
                                Console.WriteLine("spellLong null reference");
                                return false;
                            }
                            else
                            {
                                spellsList.Add(spellLong);
                            }
                        }
                        else
                        {
                            Console.WriteLine("spellShort.index null reference");
                            return false;
                        }
                    }

                    _ldbBase.UpdateDatabase(spellsList);
                }

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
                return false;
            }
        }

        return false;
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
