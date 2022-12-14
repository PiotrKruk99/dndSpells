using Xunit;
using Moq;
using FluentAssertions;
using webApi.Api.DataClasses;
using webApi.Services;
using webApi.Controllers;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace webApi.Tests;

public class DndApiControllerTests
{
    private Mock<IApiService> _service;
    private Mock<ILogger<DndApiController>> _logger;
    private DndApiController _controller;

    public DndApiControllerTests()
    {
        _service = new Mock<IApiService>();
        _logger = new Mock<ILogger<DndApiController>>();
        _controller = new DndApiController(_logger.Object, _service.Object);
    }

    [Fact]
    public async void GetAllSpells_Controller_Test()
    {
        _service.Setup(x => x.GetAllSpells())
                .ReturnsAsync(new SpellsList()
                {
                    count = 1,
                    results = new List<SpellShort>() {
                        new SpellShort() {
                            index = "fireball",
                            name = "Fireball",
                            url = @"http://some.url"
                        }
                    }
                });

        var result = await _controller.GetAllSpells() as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);
        result!.Value.Should().NotBeNull();
        (result!.Value as SpellsList)!.count.Should().Be(1);
        (result!.Value as SpellsList)!.results.Should().NotBeNull();
        (result!.Value as SpellsList)!.results!.Count.Should().Be(1);
        (result!.Value as SpellsList)!.results!.Count.Should().Be(1);
        (result!.Value as SpellsList)!.results![0].index.Should().Be("fireball");
        (result!.Value as SpellsList)!.results![0].name.Should().Be("Fireball");
        (result!.Value as SpellsList)!.results![0].url.Should().Be(@"http://some.url");
    }

    [Fact]
    public void GetSpell_ShouldReturnBadRequest_Test()
    {
        var result = _controller.GetSpell("");

        result.Should().BeOfType<BadRequestResult>();
    }
}
