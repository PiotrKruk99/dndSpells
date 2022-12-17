using Xunit;
using FluentAssertions;
using Moq;
using webApi.Database;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace webApi.Tests;

public class LiteDBOperTests
{
    private LiteDBOper liteDBOper;
    private Mock<ILogger<LiteDBOper>> _logger = new Mock<ILogger<LiteDBOper>>();

    public LiteDBOperTests()
    {
        liteDBOper = new LiteDBOper(_logger.Object);
    }

    [Fact]
    public void ConstructorTest()
    {
        liteDBOper.Should().NotBeNull();
        liteDBOper.IsDatabase.Should().BeTrue();
    }

    [Fact]
    public void GetLastUpdate_Should_Return_NotNull_DateTime()
    {
        object? result = liteDBOper.GetLastUpdate();

        result.Should().NotBeNull();
        result.Should().BeOfType<DateTime>();
        ((DateTime)result).Should().BeBefore(DateTime.Now);
    }

    [Fact]
    public void FieldTest()
    {
        var colls = liteDBOper.GetAllSpellsLong();
        // var spells = colls.Where(x => x.duration == null || x.duration.Length == 0);
        // var spells = colls.Where(x => x.casting_time == null || x.casting_time.Length == 0);
        // var spells = colls.Where(x => x.higher_level == null || x.higher_level.Count == 0);
        // var spells = colls.Where(x => x.range == null || x.range.Length == 0);
        // var spells = colls.Where(x => x.material == null || x.material.Length == 0);
        // var spells = colls.Where(x => x.damage == null || x.damage.damage_type == null 
        //                 || x.damage.damage_type.name == null || x.damage.damage_type.name.Length == 0);
        // var spells = colls.Where(x => x.school == null || x.school.name == null || x.school.name.Length == 0);
        // var spells = colls.Where(x => x.classes == null || x.classes.Count == 0);
        var spells = colls.Where(x => x.Subclasses.Count == 0);

        colls.Should().NotBeNull();
        colls.Count().Should().BeGreaterThan(0);
        spells.Should().NotBeNull();
        spells.Count().Should().Be(0);
    }
}
