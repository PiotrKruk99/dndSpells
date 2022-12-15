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

    public ApiClassesProfile()
    {
        CreateMap<SpellLong, SpellLongDto>()
            // .ForMember(dest => dest.Id, map => map.MapFrom(src => src._id))
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
            .ForMember(dest => dest.DamageType, map => map.MapFrom(src => FromDamageType(src.damage)));
    }
}