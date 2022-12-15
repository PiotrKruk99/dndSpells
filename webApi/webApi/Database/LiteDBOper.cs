using LiteDB;
using webApi.Api.DataClasses;

namespace webApi.Database;

public class LiteDBOper : ILiteDBOper
{
    private LiteDatabase? dataBase;
    private string filePath = @"AppData/apiData.ldb";
    public bool IsDatabase { get => dataBase == null ? false : true; }
    private ILogger<LiteDBOper> _logger;

    public LiteDBOper(ILogger<LiteDBOper> logger)
    {
        _logger = logger;

        if (!OpenDatabase())
            throw new Exception("Cannot open or create database. Is AppData folder exists?");
    }

    private bool OpenDatabase()
    {
        try
        {
            if (IsDatabase)
                return true;

            string? directoryName = Path.GetDirectoryName(filePath);

            if (directoryName is null)
                throw new Exception("Invalid database directory");

            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);

            var connStr = new ConnectionString
            {
                Connection = ConnectionType.Direct,
                Filename = filePath
            };

            dataBase = new LiteDatabase(connStr);
            return true;
        }
        catch (LiteException exc)
        {
            _logger.Log(LogLevel.Warning, exc.ToString());
            return false;
        }
        catch (Exception exc)
        {
            _logger.Log(LogLevel.Warning, exc.ToString());
            return false;
        }
    }

    private void SetLastUpdate()
    {
        if (dataBase is null && !OpenDatabase())
            throw new Exception("database is closed and cannot be opened");

        var col = dataBase!.GetCollection<LastUpdate>();

        var lastUpdate = col.Query().FirstOrDefault();

        if (lastUpdate is null)
        {
            lastUpdate = new LastUpdate();
            lastUpdate.UpdateDate = DateTime.Now;
            col.Insert(lastUpdate);
        }
        else
        {
            lastUpdate.UpdateDate = DateTime.Now;
            col.Update(lastUpdate);
        }
    }

    public DateTime GetLastUpdate()
    {
        if (dataBase is null && !OpenDatabase())
            throw new Exception("database is closed and cannot be opened");

        var col = dataBase!.GetCollection<LastUpdate>();

        var lastUpdate = col.Query().FirstOrDefault();

        if (lastUpdate is not null)
            return lastUpdate.UpdateDate;
        else
            return DateTime.Now.AddDays(-2);

    }

    public bool UpdateDatabase(List<SpellLong> spellsList)
    {
        if (dataBase is null && !OpenDatabase())
        {
            _logger.Log(LogLevel.Warning, "database is closed and cannot be opened");
            return false;
        }

        try
        {
            var col = dataBase!.GetCollection<SpellLong>();

            col.DeleteAll();
            col.Insert(spellsList);

            SetLastUpdate();

            dataBase.Checkpoint();
        }
        catch (LiteException exc)
        {
            _logger.Log(LogLevel.Warning, exc.ToString());
            return false;
        }
        catch (Exception exc)
        {
            _logger.Log(LogLevel.Warning, exc.ToString());
            return false;
        }

        return true;
    }

    public SpellLong? GetSpell(string index)
    {
        if (dataBase is null && !OpenDatabase())
            throw new Exception("database is closed and cannot be opened");

        var col = dataBase!.GetCollection<SpellLong>();
        return col.FindOne(x => x.index == index);
    }

    public IEnumerable<SpellLong> GetAllSpellsLong()
    {
        if (dataBase is null && !OpenDatabase())
            throw new Exception("database is closed and cannot be opened");

        var col = dataBase!.GetCollection<SpellLong>();

        return col.FindAll();
    }
}
