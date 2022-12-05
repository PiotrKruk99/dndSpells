namespace webApi.Api.DataClasses;

public class SpellsList
{
    public int? _id { get; set; }
    public int count { get; set; }
    public List<SpellShort>? results { get; set; }
}