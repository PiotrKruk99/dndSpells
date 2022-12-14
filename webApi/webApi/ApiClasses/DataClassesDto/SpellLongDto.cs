using webApi.Api.DataClasses;

namespace webApi.Api.DataClassesDto;

public class SpellLongDto
{
    public int Id { get; set; }
    public string Index { get; set; } = string.Empty;
    public string? name { get; set; }
    public List<string>? desc { get; set; }
    public List<string>? higher_level { get; set; }
    public string? range { get; set; }
    public List<string>? components { get; set; }
    public string? material { get; set; }
    public bool ritual { get; set; }
    public string? duration { get; set; }
    public bool concentration { get; set; }
    public string? casting_time { get; set; }
    public int level { get; set; }
    public Damage? damage { get; set; }
    public Dc? dc { get; set; }
    public AreaOfEffect? area_of_effect { get; set; }
    public School? school { get; set; }
    public List<Class>? classes { get; set; }
    public List<Subclass>? subclasses { get; set; }
    public string? url { get; set; }
}
