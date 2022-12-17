using webApi.Api.DataClassesDto;

namespace webApi.Database;

public interface ILiteDBOper
{
    public bool IsDatabase { get; }
    public DateTime GetLastUpdate();
    public bool UpdateDatabase(List<SpellLongDto> spellsList);
    public SpellLongDto? GetSpell(string index);
}
