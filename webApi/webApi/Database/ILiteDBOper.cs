using LiteDB;

namespace webApi.Database;

public interface ILiteDBOper
{
    public bool IsDatabase { get; }
    public DateTime GetLastUpdate();
}
