using Xunit;
using FluentAssertions;
using webApi.Api;

namespace webApi.Tests;

public class DndApiTests
{
    [Fact]
    public void GetAllSpellsTest()
    {
        var result = DndApi.GetAllSpells();
        result.Should().NotBeNull();
        result.Result.Should().NotBeNull();
        result.Result!.count.Should().BeGreaterThan(0);
        result.Result!.results!.Count.Should().BeGreaterThan(0);
    }
}