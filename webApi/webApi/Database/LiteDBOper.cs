using LiteDB;
using LiteDB.Async;
using webApi.Api.DataClassesDto;

namespace webApi.Database;

public class LiteDBOper : ILiteDBOper
{
    private string filePath = @"AppData/apiData.ldb";
    private ILogger<LiteDBOper> _logger;

    public LiteDBOper(ILogger<LiteDBOper> logger)
    {
        _logger = logger;

        // if (!OpenDatabase())
        //     throw new Exception("Cannot open or create database. Is AppData folder exists?");
    }

    private LiteDatabaseAsync? OpenDatabase(bool readOnly = false)
    {
        try
        {
            string? directoryName = Path.GetDirectoryName(filePath);

            if (directoryName is null)
                throw new Exception("Invalid database directory");

            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);

            var connStr = new ConnectionString
            {
                Connection = ConnectionType.Shared,
                Filename = filePath,
                ReadOnly = readOnly
            };

            var dataBase = new LiteDatabaseAsync(connStr);
            return dataBase;
        }
        catch (LiteException exc)
        {
            _logger.Log(LogLevel.Warning, exc, exc.ToString());
            return null;
        }
        catch (Exception exc)
        {
            _logger.Log(LogLevel.Warning, exc, exc.ToString());
            return null;
        }
    }

    private async Task SetLastUpdate()
    {
        try
        {
            using (var dataBase = OpenDatabase())
            {
                if (dataBase is null)
                    throw new Exception("database is closed and cannot be opened");

                var col = dataBase.GetCollection<LastUpdate>();

                var lastUpdate = await col.Query().FirstOrDefaultAsync();

                if (lastUpdate is null)
                {
                    lastUpdate = new LastUpdate();
                    lastUpdate.UpdateDate = DateTime.Now;
                    await col.InsertAsync(lastUpdate);
                }
                else
                {
                    lastUpdate.UpdateDate = DateTime.Now;
                    await col.UpdateAsync(lastUpdate);
                }
            }
        }
        catch (LiteException exc)
        {
            _logger.Log(LogLevel.Warning, exc, exc.ToString());
            return;
        }
        catch (Exception exc)
        {
            _logger.Log(LogLevel.Warning, exc, exc.ToString());
            return;
        }
    }

    public async Task<DateTime?> GetLastUpdate()
    {
        try
        {
            using (var dataBase = OpenDatabase())
            {
                if (dataBase is null)
                    throw new Exception("database is closed and cannot be opened");

                var col = dataBase.GetCollection<LastUpdate>();

                var lastUpdate = await col.Query().FirstOrDefaultAsync();

                if (lastUpdate is not null)
                    return lastUpdate.UpdateDate;
                else
                    return DateTime.Now.AddDays(-2);
            }
        }
        catch (LiteException exc)
        {
            _logger.Log(LogLevel.Warning, exc, exc.ToString());
            return null;
        }
        catch (Exception exc)
        {
            _logger.Log(LogLevel.Warning, exc, exc.ToString());
            return null;
        }

    }

    public async Task<bool> UpdateDatabase<T>(List<T> elemsList)
    {
        try
        {
            using (var dataBase = OpenDatabase())
            {
                if (dataBase is null)
                    throw new Exception("database is closed and cannot be opened");

                var cols = dataBase!.GetCollection<T>();

                await cols.DeleteAllAsync();
                await cols.InsertAsync(elemsList);

                await SetLastUpdate();

                await dataBase.CheckpointAsync();

                return true;
            }
        }
        catch (LiteException exc)
        {
            _logger.Log(LogLevel.Warning, exc, exc.ToString());
            return false;
        }
        catch (Exception exc)
        {
            _logger.Log(LogLevel.Warning, exc, exc.ToString());
            return false;
        }
    }

    public async Task<SpellLongDto?> GetSpell(string index)
    {
        try
        {
            using (var dataBase = OpenDatabase())
            {
                if (dataBase is null)
                    throw new Exception("database is closed and cannot be opened");

                var col = dataBase!.GetCollection<SpellLongDto>();
                return await col.FindOneAsync(x => x.Index == index);
            }
        }
        catch (LiteException exc)
        {
            _logger.Log(LogLevel.Warning, exc, exc.ToString());
            return null;
        }
        catch (Exception exc)
        {
            _logger.Log(LogLevel.Warning, exc, exc.ToString());
            return null;
        }
    }

    public async Task<IEnumerable<T>?> GetAllSpells<T>()
    {
        try
        {
            using (var dataBase = OpenDatabase())
            {
                if (dataBase is null)
                    throw new Exception("database is closed and cannot be opened");

                var col = dataBase!.GetCollection<T>();

                return await col.FindAllAsync();
            }
        }
        catch (LiteException exc)
        {
            _logger.Log(LogLevel.Warning, exc, exc.ToString());
            return null;
        }
        catch (Exception exc)
        {
            _logger.Log(LogLevel.Warning, exc, exc.ToString());
            return null;
        }
    }
}
