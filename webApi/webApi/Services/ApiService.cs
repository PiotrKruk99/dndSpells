using webApi.Database;

namespace webApi.Services;

public abstract class ApiService
{
    public ApiService(ILiteDBOper ldbBase, ILogger<DndApiService> logger)
    { }
}