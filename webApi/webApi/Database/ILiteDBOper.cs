using webApi.Api.DataClasses;

namespace webApi.Database;

public interface ILiteDBOper
{
    public bool IsDatabase { get; }
    public DateTime GetLastUpdate();
    public bool UpdateDatabase(List<SpellLong> spellsList);
    public SpellLong? GetSpell(string index);
}
