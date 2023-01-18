using AutoMapper;
using webApi.Api;
using webApi.Api.DataClassesDto;
using webApi.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace webApi.Services;

public class DndApiService : ApiService, IApiService
{
    private readonly DataContext _context;
    private readonly ILogger<DndApiService> _logger;
    private readonly IMapper _mapper;
    private readonly IServiceProvider _serviceProvider;
    private static Task<bool> updateDatabaseTask = new Task<bool>(() => { return true; });
    private static Task updateDatabaseTaskWatcher = Task.CompletedTask;
    private Action updateDatabaseAction;

    public DndApiService(DataContext context,
                            ILogger<DndApiService> logger,
                            IMapper mapper,
                            IServiceProvider serviceProvider) : base(context, logger)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
        _serviceProvider = serviceProvider;

        updateDatabaseAction = () =>
            {
                if (updateDatabaseTask.Exception is not null)
                {
                    foreach (var exc in updateDatabaseTask.Exception.InnerExceptions)
                    {
                        _logger.Log(LogLevel.Information, exc, exc.ToString());
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

        if (updateDatabaseTaskWatcher.IsCompleted && ShouldBeUpdated())
        {
            DataContext newContext = _context;
            updateDatabaseTask = UpdateDatabase();
            updateDatabaseTaskWatcher = updateDatabaseTask.ContinueWith(x => updateDatabaseAction());
        }
    }

    private bool ShouldBeUpdated()
    {
        try
        {
            var lastUpdate = _context.LastUpdates.AsNoTracking()
                                                 .OrderBy(x => x.Id)
                                                 .FirstOrDefault();

            if (lastUpdate is null)
                return true;

            if (lastUpdate.UpdateDate.AddDays(1) < DateTime.Now)
                return true;
            else
                return false;
        }
        catch (Exception exc)
        {
            _logger.LogError(exc, "error during  getting last update date");
            return false;
        }
    }

    private async Task<bool> UpdateDatabase()
    {
        ////// geting data from dnd api ////////

        var allSpells = await DndApi.GetAllSpells();

        if (allSpells is null || allSpells.results is null)
        {
            _logger.Log(LogLevel.Warning, "GetAllSpells returns empty list");
            return false;
        }

        List<SpellLongDto> spellLongDtos = new List<SpellLongDto>();
        List<SpellShortDto> spellShortDtos = new List<SpellShortDto>();

        foreach (var spellShort in allSpells.results)
        {
            if (spellShort.index is not null)
            {
                var spellLong = await DndApi.GetSpell(spellShort.index);
                var spellLongDto = _mapper.Map<SpellLongDto>(spellLong);
                var spellShortDto = _mapper.Map<SpellShortDto>(spellShort);

                spellShortDtos.Add(spellShortDto);

                if (spellLongDto is null)
                {
                    _logger.Log(LogLevel.Warning, "spellLongDto null reference");
                    return false;
                }
                else
                {
                    spellLongDtos.Add(spellLongDto);
                }
            }
            else
            {
                _logger.Log(LogLevel.Warning, "spellShort.index null reference");
                return false;
            }
        }

        ///////// writing data to local database ///////////

        using var context = new DataContext();
        // using var context = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
        using var transaction = context.Database.BeginTransaction();

        // Task[] addTasks = new Task[]
        // {
        //     Task.Factory.StartNew(async () => {
        //                             context.RemoveRange(context.SpellLongDtos.AsNoTracking().ToList());
        //                             await context.SaveChangesAsync();
        //                             context.SpellLongDtos.AddRange(spellLongDtos);
        //                             await context.SaveChangesAsync();
        //     }),
        //     Task.Factory.StartNew(async () => {
        //                             context.RemoveRange(context.SpellShortDtos.AsNoTracking().ToList());
        //                             await context.SaveChangesAsync();
        //                             context.SpellShortDtos.AddRange(spellShortDtos);
        //                             await context.SaveChangesAsync();
        //     }),
        //     Task.Factory.StartNew(async () => {
        //                             LastUpdate? lastUpdate = context.LastUpdates.FirstOrDefault();
        //                             if (lastUpdate is null)
        //                             {
        //                                 lastUpdate = new LastUpdate() { UpdateDate = DateTime.Now };
        //                                 context.LastUpdates.Add(lastUpdate);
        //                             }
        //                             else
        //                             {
        //                                 lastUpdate.UpdateDate = DateTime.Now;
        //                                 context.LastUpdates.Update(lastUpdate);
        //                             }
        //                             await context.SaveChangesAsync();
        //     }),
        // };

        // Task.WaitAll(addTasks);

        context.RemoveRange(context.SpellLongDtos.AsNoTracking().ToList());
        context.SpellLongDtos.AddRange(spellLongDtos);

        context.RemoveRange(context.SpellShortDtos.AsNoTracking().ToList());
        context.SpellShortDtos.AddRange(spellShortDtos);

        LastUpdate? lastUpdate = context.LastUpdates.OrderBy(x => x.Id)
                                                    .FirstOrDefault();
        if (lastUpdate is null)
        {
            lastUpdate = new LastUpdate() { UpdateDate = DateTime.Now };
            context.LastUpdates.Add(lastUpdate);
        }
        else
        {
            lastUpdate.UpdateDate = DateTime.Now;
            context.LastUpdates.Update(lastUpdate);
        }

        await context.SaveChangesAsync();

        transaction.Commit();

        return true;
    }

    public async Task<IEnumerable<SpellShortDto>?> GetAllSpells()
    {
        var result = await _context.SpellShortDtos.AsNoTracking().ToListAsync();

        if (result is null || result.Count() == 0)
            return null;
        else
            return result;
    }

    public async Task<SpellLongDto?> GetSpell(string index)
    {
        return await _context.SpellLongDtos.AsNoTracking().FirstOrDefaultAsync(x => x.Index == index);
    }
}
