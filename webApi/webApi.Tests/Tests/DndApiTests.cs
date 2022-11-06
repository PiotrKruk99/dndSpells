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

    [Theory]
    [InlineData("fireball")]
    [InlineData("fire-bolt")]
    public void GetSpellTest(string index)
    {
        var result = DndApi.GetSpell(index);
        result.Should().NotBeNull();
        result.Result.Should().NotBeNull();
        result.Result!.name.Should().BeOneOf("Fire Bolt", "Fireball");
        result.Result!.area_of_effect?.size.Should().BeGreaterThan(3);
    }
}
