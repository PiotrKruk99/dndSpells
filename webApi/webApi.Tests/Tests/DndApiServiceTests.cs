using Xunit;
using FluentAssertions;
using Moq;
using webApi.Database;
using webApi.Services;
using webApi.Api.DataClasses;
using Microsoft.Extensions.Logging;

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
    public async void GetSpell_ShouldReturnNull_Test(string index)
    {
        var result = await _dndApiService.GetSpell(index);
        result.Should().BeNull();
    }

    [Fact]
    public async void GetSpell_ShouldReturnValidSpell_Test()
    {
        SpellLong? result = await _dndApiService.GetSpell("fireball");
        result.Should().NotBeNull();
        result!.level.Should().Be(3);
        result.classes.Should().NotBeNull();
        result.classes!.Count.Should().BeGreaterThan(1);
    }
}
