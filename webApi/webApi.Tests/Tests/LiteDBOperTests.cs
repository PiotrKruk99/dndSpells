using Xunit;
using FluentAssertions;
using Moq;
using webApi.Database;
using System;
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
}
