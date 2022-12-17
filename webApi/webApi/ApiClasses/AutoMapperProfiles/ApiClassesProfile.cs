using AutoMapper;
using webApi.Api.DataClasses;
using webApi.Api.DataClassesDto;

namespace webApi.Api.AutomapperProfiles;

public class ApiClassesProfile : Profile
{
    private string FromNotEmptyString(string? str)
    {
        if (str is null || str.Length == 0)
            throw new Exception("corupted data (string) in SpellLong");
        else
            return str;
    }

    private List<string> FromNotEmptyList(List<string>? list)
    {
        if (list is null || list.Count == 0)
            throw new Exception("corupted data (List<string>) in SpellLong");
        else
            return list;
    }

    private string FromNullableString(string? str)
    {
        if (str is null)
            return string.Empty;
        else
            return str;
    }

    private List<string> FromNullableList(List<string>? list)
    {
        if (list is null)
            return new List<string>();
        else
            return list;
    }

    private string FromDamageType(Damage? damage)
    {
        if (damage is null || damage.damage_type is null || damage.damage_type.name is null)
            return string.Empty;
        else
            return damage.damage_type.name;
    }

    private string FromDCType(Dc? dc)
    {
        if (dc is null || dc.dc_type is null || dc.dc_type.name is null)
            return string.Empty;
        else
            return dc.dc_type.name;
    }

    private string FromDCSuccess(Dc? dc)
    {
        if (dc is null || dc.dc_success is null)
            return string.Empty;
        else
            return dc.dc_success;
    }

    private string FromAreaOfEffectType(AreaOfEffect? area)
    {
        if (area is null || area.type is null)
            return string.Empty;
        else
            return area.type;
    }

    private int FromAreaOfEffectSize(AreaOfEffect? area)
    {
        if (area is null)
            return 0;
        else
            return area.size;
    }

    private string FromNotEmptySchoolName(School? school)
    {
        if (school is null || school.name is null || school.name.Length == 0)
            throw new Exception("corupted data (school name) in SpellLong");
        else
            return school.name;
    }

    private List<string> FromNotEmptyClasses(List<Class>? classList)
    {
        if (classList is null || classList.Count == 0
                                || classList.Where(x => x.name == null || x.name.Length == 0).Count() > 0)
        {
            throw new Exception("corupted data (character class list) in SpellLong");
        }
        else
        {
            var result = new List<string>();

            foreach (var className in classList)
                result.Add(className.name!);

            return result;
        }
    }

    private List<string> FromSubclasses(List<Subclass>? subclassList)
    {
        if (subclassList is null || subclassList.Where(x => x.name == null || x.name.Length == 0).Count() > 0)
        {
            return new List<string>();
        }
        else
        {
            var result = new List<string>();

            foreach (var subclass in subclassList)
                result.Add(subclass.name!);

            return result;
        }
    }

    public ApiClassesProfile()
    {
        CreateMap<SpellLong, SpellLongDto>()
            .ForMember(dest => dest.Index, map => map.MapFrom(src => FromNotEmptyString(src.index)))
            .ForMember(dest => dest.Name, map => map.MapFrom(src => FromNotEmptyString(src.name)))
            .ForMember(dest => dest.Desc, map => map.MapFrom(src => FromNotEmptyList(src.desc)))
            .ForMember(dest => dest.HigherLevel, map => map.MapFrom(src => FromNullableList(src.higher_level)))
            .ForMember(dest => dest.Range, map => map.MapFrom(src => FromNotEmptyString(src.range)))
            .ForMember(dest => dest.Components, map => map.MapFrom(src => FromNotEmptyList(src.components)))
            .ForMember(dest => dest.Material, map => map.MapFrom(src => FromNullableString(src.material)))
            .ForMember(dest => dest.Ritual, map => map.MapFrom(src => src.ritual))
            .ForMember(dest => dest.Duration, map => map.MapFrom(src => FromNotEmptyString(src.duration)))
            .ForMember(dest => dest.Concentration, map => map.MapFrom(src => src.concentration))
            .ForMember(dest => dest.CastingTime, map => map.MapFrom(src => FromNotEmptyString(src.casting_time)))
            .ForMember(dest => dest.Level, map => map.MapFrom(src => src.level))
            .ForMember(dest => dest.DamageType, map => map.MapFrom(src => FromDamageType(src.damage)))
            .ForMember(dest => dest.DcType, map => map.MapFrom(src => FromDCType(src.dc)))
            .ForMember(dest => dest.OnDcSuccess, map => map.MapFrom(src => FromDCSuccess(src.dc)))
            .ForMember(dest => dest.AreaOfEffectType, map => map.MapFrom(src => FromAreaOfEffectType(src.area_of_effect)))
            .ForMember(dest => dest.AreaOfEffectSize, map => map.MapFrom(src => FromAreaOfEffectSize(src.area_of_effect)))
            .ForMember(dest => dest.School, map => map.MapFrom(src => FromNotEmptySchoolName(src.school)))
            .ForMember(dest => dest.Classes, map => map.MapFrom(src => FromNotEmptyClasses(src.classes)))
            .ForMember(dest => dest.Subclasses, map => map.MapFrom(src => FromSubclasses(src.subclasses)));
    }
}
