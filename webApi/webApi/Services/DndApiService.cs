using webApi.Api;
using webApi.Api.DataClasses;
using webApi.Database;

namespace webApi.Services;

public class DndApiService : ApiService, IApiService
{
    private readonly ILiteDBOper _ldbBase;
    private readonly ILogger<DndApiService> _logger;
    private Task<bool> updateDatabaseTask = new Task<bool>(() => { return true; });
    private static Task updateDatabaseTaskWatcher = Task.CompletedTask;
    private Action updateDatabaseAction;

    public DndApiService(ILiteDBOper ldbBase, ILogger<DndApiService> logger) : base(ldbBase, logger)
    {
        _ldbBase = ldbBase;
        _logger = logger;

        updateDatabaseAction = () =>
            {
                if (updateDatabaseTask.Exception is not null)
                {
                    foreach (var exc in updateDatabaseTask.Exception.Flatten().InnerExceptions)
                    {
                        _logger.Log(LogLevel.Warning, exc.ToString());
                    }
                }

                if (updateDatabaseTask.Exception is not null || !updateDatabaseTask.Result)
                {
                    _logger.Log(LogLevel.Error, "error while updating database");
                }
                else
                {
                    _logger.Log(LogLevel.Information, "database updated successfully");
                }
            };

        if (updateDatabaseTaskWatcher.IsCompleted && _ldbBase.GetLastUpdate().AddDays(1) < DateTime.Now)
        {
            updateDatabaseTask = UpdateDatabase();
            updateDatabaseTaskWatcher = updateDatabaseTask.ContinueWith(x => updateDatabaseAction());
        }
    }

    private async Task<bool> UpdateDatabase()
    {
        var allSpells = await DndApi.GetAllSpells();

        if (allSpells is null || allSpells.results is null)
        {
            _logger.Log(LogLevel.Warning, "GetAllSpells returns empty list");
            return false;
        }

        List<SpellLong> spellsList = new List<SpellLong>();

        foreach (var spellShort in allSpells.results)
        {
            if (spellShort.index is not null)
            {
                var spellLong = await DndApi.GetSpell(spellShort.index);

                if (spellLong is null)
                {
                    _logger.Log(LogLevel.Warning, "spellLong null reference");
                    return false;
                }
                else
                {
                    spellsList.Add(spellLong);
                }
            }
            else
            {
                _logger.Log(LogLevel.Warning, "spellShort.index null reference");
                return false;
            }
        }

        if (_ldbBase.UpdateDatabase(spellsList))
            return true;
        else
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
            _logger.Log(LogLevel.Error, exc.ToString());
            return null;
        }

        return allSpells;
    }

    // public async Task<SpellLong?> GetSpell(string index)
    // {
    //     SpellLong? spell;
    //     try
    //     {
    //         spell = await DndApi.GetSpell(index);
    //     }
    //     catch (Exception exc)
    //     {
    //         _logger.Log(LogLevel.Error, exc.ToString());
    //         return null;
    //     }

    //     return spell;
    // }

    public SpellLong? GetSpell(string index)
    {
        return _ldbBase.GetSpell(index);
    }
}
