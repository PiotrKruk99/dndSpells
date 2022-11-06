using Xunit;
using FluentAssertions;
using Moq;
using webApi.Database;
using webApi.Services;

namespace webApi.Tests;

public class DndApiServiceTests
{
    private readonly Mock<LiteDBOper> _liteDBOper = new Mock<LiteDBOper>();
    private DndApiService _dndApiService;

    public DndApiServiceTests()
    {
        _dndApiService = new DndApiService(_liteDBOper.Object);
    }

    [Fact]
    public async void GetAllSpells_Service_Test()
    {
        var result = await _dndApiService.GetAllSpells();
        result.Should().NotBeNull();
        result.Should().NotBeNull();
        result!.count.Should().BeGreaterThan(0);
    }
}
