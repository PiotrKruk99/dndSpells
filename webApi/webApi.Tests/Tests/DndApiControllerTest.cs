using Xunit;
using Moq;
using FluentAssertions;
using webApi.Services;
using System.Collections.Generic;

namespace webApi.Tests;

public class DndApiControllerTests
{
    private Mock<IApiService> _service = new Mock<IApiService>();

    [Fact]
    public async void GetAllSpellsNamesTest()
    {
        _service.Setup(x => x.GetAllSpells())
                .ReturnsAsync(new Api.SpellsList() {
                    count = 1,
                    results = new List<Api.SpellShort>() {
                        new Api.SpellShort() {
                            index = "fireball",
                            name = "Fireball",
                            url = @"http://some.url"
                        }
                    }
                });
        
        var result = await _service.Object.GetAllSpells();

        result.Should().NotBeNull();
        result!.count.Should().Be(1);
    }
}
