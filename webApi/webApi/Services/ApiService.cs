using webApi.Database;

namespace webApi.Services;

public abstract class ApiService
{
    public ApiService(DataContext context, ILogger<DndApiService> logger)
    { }
}