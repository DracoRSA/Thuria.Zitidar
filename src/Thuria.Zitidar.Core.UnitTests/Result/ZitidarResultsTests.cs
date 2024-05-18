using NUnit.Framework;
using FluentAssertions;

using Thuria.Zitidar.Core.Models;
using Thuria.Zitidar.Core.Result;

namespace Thuria.Zitidar.Core.UnitTests.Result;

[TestFixture]
public class ZitidarResultsTests
{
    [Test]
    public void Constructor()
    {
        // Arrange
            
        // Act
        var zitidarResult = new ZitidarResults<bool>(true);

        // Assert
        zitidarResult.Should().NotBeNull();
    }

    [Test]
    public void Constructor_GivenSuccess_ShouldSetPropertiesToExpectedValues()
    {
        // Arrange

        // Act
        var zitidarResult = new ZitidarResults<bool>(true);

        // Assert
        zitidarResult.IsError.Should().BeFalse();
        zitidarResult.IsSuccess.Should().BeTrue();
        zitidarResult.Value.Should().BeTrue();
        zitidarResult.Errors.Should().BeNull();
    }

    [Test]
    public void Constructor_GivenError_ShouldSetPropertiesToExpectedValues()
    {
        // Arrange
        var resultErrors = new List<ZitidarError>
                           {
                               new(1, "Test Error Message")
                           };

        // Act
        var zitidarResult = new ZitidarResults<bool>(resultErrors);

        // Assert
        zitidarResult.IsError.Should().BeTrue();
        zitidarResult.IsSuccess.Should().BeFalse();
        zitidarResult.Value.Should().BeFalse();
        zitidarResult.Errors.Should().BeEquivalentTo(resultErrors);
    }

    [Test]
    public void Ok_ShouldSetPropertiesToExpectedValues()
    {
        // Arrange

        // Act
        var zitidarResult = ZitidarResults<bool>.Ok(true);

        // Assert
        zitidarResult.IsError.Should().BeFalse();
        zitidarResult.IsSuccess.Should().BeTrue();
        zitidarResult.Value.Should().BeTrue();
        zitidarResult.Errors.Should().BeNull();
    }

    [Test]
    public void Fail_ShouldSetPropertiesToExpectedValues()
    {
        // Arrange
        var resultErrors = new List<ZitidarError>
                           {
                               new(1, "Test Error Message")
                           };

        // Act
        var zitidarResult = ZitidarResults<bool>.Fail(resultErrors);

        // Assert
        zitidarResult.IsError.Should().BeTrue();
        zitidarResult.IsSuccess.Should().BeFalse();
        zitidarResult.Value.Should().BeFalse();
        zitidarResult.Errors.Should().BeEquivalentTo(resultErrors);
    }
}