using LiteDB;
using webApi.Api.DataClasses;

namespace webApi.Database;

public class LiteDBOper : ILiteDBOper
{
    private LiteDatabase? dataBase;
    private string filePath = @"AppData/apiData.ldb";
    public bool IsDatabase { get => dataBase == null ? false : true; }

    public LiteDBOper()
    {
        if (!OpenDatabase())
            throw new Exception("Cannot open database");
    }

    private bool OpenDatabase()
    {
        if (IsDatabase)
            return true;

        string? directoryName = Path.GetDirectoryName(filePath);

        if (directoryName is null)
            throw new Exception("Invalid database directory");

        if (!Directory.Exists(directoryName))
            throw new Exception(Path.GetFullPath(directoryName) + " not exists");

        var connStr = new ConnectionString
        {
            Connection = ConnectionType.Direct,
            Filename = filePath
        };

        try
        {
            dataBase = new LiteDatabase(connStr);
            return true;
        }
        catch (LiteException exc)
        {
            throw exc;
        }
        catch (Exception)
        {
            return false;
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
        throw new NotImplementedException("UpdateDatabase");
    }
}
