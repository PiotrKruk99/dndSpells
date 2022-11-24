using LiteDB;

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

        if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            throw new Exception(Path.GetFullPath(filePath) + " not exists");

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
}
