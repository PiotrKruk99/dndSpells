using LiteDB;

namespace webApi.Database;

public interface ILiteDBOper
{
    bool IsDatabase { get; }
}