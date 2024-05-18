using NUnit.Framework;
using FluentAssertions;

using Thuria.Zitidar.Core.Models;
using Thuria.Zitidar.Core.Result;

namespace Thuria.Zitidar.Core.UnitTests.Result;

[TestFixture]
public class ZitidarResultTests
{
    [Test]
    public void Constructor()
    {
        // Arrange
            
        // Act
        var zitidarResult = new ZitidarResult<bool>(true);

        // Assert
        zitidarResult.Should().NotBeNull();
    }

    [Test]
    public void Constructor_GivenSuccess_ShouldSetPropertiesToExpectedValues()
    {
        // Arrange

        // Act
        var zitidarResult = new ZitidarResult<bool>(true);

        // Assert
        zitidarResult.IsError.Should().BeFalse();
        zitidarResult.IsSuccess.Should().BeTrue();
        zitidarResult.Value.Should().BeTrue();
        zitidarResult.Error.Should().BeNull();
    }

    [Test]
    public void Constructor_GivenError_ShouldSetPropertiesToExpectedValues()
    {
        // Arrange
        var resultError = new ZitidarError(1, "Test Error Message");

        // Act
        var zitidarResult = new ZitidarResult<bool>(resultError);

        // Assert
        zitidarResult.IsError.Should().BeTrue();
        zitidarResult.IsSuccess.Should().BeFalse();
        zitidarResult.Value.Should().BeFalse();
        zitidarResult.Error.Should().Be(resultError);
    }

    [Test]
    public void Ok_ShouldSetPropertiesToExpectedValues()
    {
        // Arrange

        // Act
        var zitidarResult = ZitidarResult<bool>.Ok(true);

        // Assert
        zitidarResult.IsError.Should().BeFalse();
        zitidarResult.IsSuccess.Should().BeTrue();
        zitidarResult.Value.Should().BeTrue();
        zitidarResult.Error.Should().BeNull();
    }

    [Test]
    public void Fail_ShouldSetPropertiesToExpectedValues()
    {
        // Arrange
        var resultError =  new ZitidarError(1, "Test Error Message");

        // Act
        var zitidarResult = ZitidarResult<bool>.Fail(resultError);

        // Assert
        zitidarResult.IsError.Should().BeTrue();
        zitidarResult.IsSuccess.Should().BeFalse();
        zitidarResult.Value.Should().BeFalse();
        zitidarResult.Error.Should().Be(resultError);
    }
}