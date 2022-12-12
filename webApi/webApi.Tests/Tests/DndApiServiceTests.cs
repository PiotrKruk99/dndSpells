using Xunit;
using FluentAssertions;
using Moq;
using webApi.Database;
using webApi.Services;
using webApi.Api.DataClasses;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace webApi.Tests;

public class DndApiServiceTests
{
    private readonly Mock<ILiteDBOper> _liteDBOper = new Mock<ILiteDBOper>();
    private DndApiService _dndApiService;
    private Mock<ILogger<DndApiService>> _logger = new Mock<ILogger<DndApiService>>();

    public DndApiServiceTests()
    {
        _dndApiService = new DndApiService(_liteDBOper.Object, _logger.Object);
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
        _liteDBOper.Setup(x => x.GetSpell("fireball")).Returns(new SpellLong() {
            level = 3,
            classes = new List<Class>() {
                new Class(), 
                new Class()
            }
        });

        SpellLong? result = _dndApiService.GetSpell("fireball");
        result.Should().NotBeNull();
        result!.level.Should().Be(3);
        result.classes.Should().NotBeNull();
        result.classes!.Count.Should().BeGreaterThan(1);
    }
}
