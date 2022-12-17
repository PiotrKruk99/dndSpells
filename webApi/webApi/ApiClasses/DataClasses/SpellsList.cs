namespace webApi.Api.DataClasses;

public class SpellsList
{
    public int count { get; set; }
    public List<SpellShort>? results { get; set; }
}