namespace webApi.Api.DataClassesDto;

public class SpellLongDto
{
    public int Id { get; set; }
    public required string Index { get; set; }
    public required string Name { get; set; }
    public required List<string> Desc { get; set; }
    public required List<string> HigherLevel { get; set; }
    public required string Range { get; set; }
    public required List<string> Components { get; set; }
    public required string Material { get; set; }
    public bool Ritual { get; set; }
    public required string Duration { get; set; }
    public bool Concentration { get; set; }
    public required string CastingTime { get; set; }
    public int Level { get; set; }
    public required string DamageType { get; set; }
    public required string DcType { get; set; }
    public required string OnDcSuccess { get; set; }
    public required string AreaOfEffectType { get; set; }
    public int AreaOfEffectSize { get; set; }
    public required string School { get; set; }
    public required List<string> Classes { get; set; }
    public required List<string> Subclasses { get; set; }
}
