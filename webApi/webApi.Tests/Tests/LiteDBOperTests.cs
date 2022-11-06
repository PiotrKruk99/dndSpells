using Xunit;
using FluentAssertions;
using webApi.Database;

namespace webApi.Tests;

public class LiteDBOperTests
{
    [Fact]
    public void Constructortest()
    {
        var result = new LiteDBOper();
        result.Should().NotBeNull();
        result.IsDatabase.Should().BeTrue();
    }
}
