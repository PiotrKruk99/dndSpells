using webApi.Api.DataClassesDto;

namespace webApi.Database;

public interface ILiteDBOper
{
    public Task<DateTime?> GetLastUpdate();
    public Task<bool> UpdateDatabase<T>(List<T> elemsList);
    public Task<SpellLongDto?> GetSpell(string index);
    public Task<IEnumerable<T>?> GetAllSpells<T>();
}