using Xunit;
using Moq;
using FluentAssertions;
using webApi.Api;
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
    public async void GetAllSpellsNames_Controller_Test()
    {
        _service.Setup(x => x.GetAllSpells())
                .ReturnsAsync(new Api.SpellsList()
                {
                    count = 1,
                    results = new List<SpellShort>() {
                        new Api.SpellShort() {
                            index = "fireball",
                            name = "Fireball",
                            url = @"http://some.url"
                        }
                    }
                });

        var result = await _controller.GetAllSpellsNames() as ObjectResult;

        result.Should().NotBeNull();
        result!.Value.Should().NotBeNull();
        (result!.Value as SpellsList)!.count.Should().Be(1);
        (result!.Value as SpellsList)!.results.Should().NotBeNull();
        (result!.Value as SpellsList)!.results!.Count.Should().Be(1);
        (result!.Value as SpellsList)!.results!.Count.Should().Be(1);
        (result!.Value as SpellsList)!.results![0].name.Should().Be("Fireball");
    }
}
