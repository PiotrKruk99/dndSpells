using Xunit;
using FluentAssertions;
using Moq;
using webApi.Database;
using webApi.Services;
using webApi.Api.DataClasses;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using AutoMapper;
using webApi.Api.DataClassesDto;

namespace webApi.Tests;

public class DndApiServiceTests
{
    private readonly Mock<ILiteDBOper> _liteDBOper = new Mock<ILiteDBOper>();
    private DndApiService _dndApiService;
    private Mock<ILogger<DndApiService>> _logger = new Mock<ILogger<DndApiService>>();
    private Mock<IMapper> _mapper = new Mock<IMapper>();

    public DndApiServiceTests()
    {
        _dndApiService = new DndApiService(_liteDBOper.Object, _logger.Object, _mapper.Object);
    }

    [Fact]
    public async void GetAllSpells_Service_Test()
    {
        var result = await _dndApiService.GetAllSpells();
        result.Should().NotBeNull();
        result.Should().NotBeNull();
        result!.count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData("")]
    [InlineData("lalala")]
    public void GetSpell_ShouldReturnNull_Test(string index)
    {
        var result = _dndApiService.GetSpell(index);
        result.Should().BeNull();
    }

    [Fact]
    public void GetSpell_ShouldReturnValidSpell_Test()
    {
        _liteDBOper.Setup(x => x.GetSpell("fireball")).Returns(new SpellLongDto()
        {
            Level = 3,
            Classes = new List<string>() {
                "Sorcerer",
                "Wizard"
            },
            AreaOfEffectType = "sphere",
            AreaOfEffectSize = 20,
            CastingTime = "1 action",
            Components = new List<string> {
                "V",
                "S",
                "M"
            },
            DamageType = "Fire",
            DcType = "DEX",
            Desc = new List<string> {
                "A bright streak flashes from your pointing finger to a point you choose within range and then blossoms with a low roar into an explosion of flame. Each creature in a 20-foot-radius sphere centered on that point must make a dexterity saving throw. A target takes 8d6 fire damage on a failed save, or half as much damage on a successful one.",
                "The fire spreads around corners. It ignites flammable objects in the area that aren't being worn or carried."
            },
            Duration = "Instantaneous",
            HigherLevel = new List<string> {
                "When you cast this spell using a spell slot of 4th level or higher, the damage increases by 1d6 for each slot level above 3rd."
            },
            Index = "fireball",
            Material = "A tiny ball of bat guano and sulfur.",
            Name = "Fireball",
            OnDcSuccess = "half",
            Range = "150 feet",
            School = "Evocation",
            Subclasses = new List<string> {
                "Lore",
                "Fiend"
            }
        });

        SpellLongDto? result = _dndApiService.GetSpell("fireball");
        result.Should().NotBeNull();
        result!.Level.Should().Be(3);
        result.Classes.Should().NotBeNull();
        result.Classes!.Count.Should().BeGreaterThan(1);
    }
}
