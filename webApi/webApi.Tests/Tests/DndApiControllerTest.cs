using Xunit;
using Moq;
using FluentAssertions;
using webApi.Api.DataClasses;
using webApi.Services;
using webApi.Controllers;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using webApi.Api.DataClassesDto;

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
                .ReturnsAsync(new List<SpellShortDto>()
                    {
                        new SpellShortDto() {
                            Index = "fireball",
                            Name = "Fireball",
                        }
                    });

        var result = await _controller.GetAllSpells() as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<List<SpellShortDto>>();
        (result.Value as List<SpellShortDto>).Should().HaveCount(1);
        (result.Value as List<SpellShortDto>)![0].Index.Should().Be("fireball");
        (result.Value as List<SpellShortDto>)![0].Name.Should().Be("Fireball");
    }

    [Fact]
    public async void GetSpell_ShouldReturnBadRequest_Test()
    {
        var result = await _controller.GetSpell("");

        result.Should().BeOfType<BadRequestResult>();
    }
}
