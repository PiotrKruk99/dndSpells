using Xunit;
using FluentAssertions;
using webApi.Database;
using System;

namespace webApi.Tests;

public class LiteDBOperTests
{
    private LiteDBOper liteDBOper;

    public LiteDBOperTests()
    {
        liteDBOper = new LiteDBOper();
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
