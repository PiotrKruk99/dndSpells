using AutoMapper;
using webApi.Api.DataClasses;
using webApi.Api.DataClassesDto;

namespace webApi.Api.AutomapperProfiles;

public class ApiClassesProfile : Profile
{
    private string NotEmptyStringCheck(string? str)
    {
        if (str is null || str.Length == 0)
            throw new Exception("corupted data (string) in SpellLong");
        else
            return str;
    }

    private List<string> NotEmptyListCheck(List<string>? list)
    {
        if (list is null || list.Count == 0)
            throw new Exception("corupted data (List<string>) in SpellLong");
        else
            return list;
    }

    private List<string> FromNullableList(List<string>? list)
    {
        if (list is null)
            return new List<string>();
        else
            return list;
    }

    public ApiClassesProfile()
    {
        CreateMap<SpellLong, SpellLongDto>()
            .ForMember(dest => dest.Id, map => map.MapFrom(src => src._id))
            .ForMember(dest => dest.Index, map => map.MapFrom(src => NotEmptyStringCheck(src.index)))
            .ForMember(dest => dest.Name, map => map.MapFrom(src => NotEmptyStringCheck(src.name)))
            .ForMember(dest => dest.Desc, map => map.MapFrom(src => NotEmptyListCheck(src.desc)))
            .ForMember(dest => dest.HigherLevel, map => map.MapFrom(src => FromNullableList(src.higher_level)));
    }
}