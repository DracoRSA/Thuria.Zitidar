using NUnit.Framework;
using FluentAssertions;

using Thuria.Calot.TestUtilities;
using Thuria.Zitidar.Core.Models;

namespace Thuria.Zitidar.Core.UnitTests.Result;

[TestFixture]
public class ZitidarErrorTests
{
    [Test]
    public void Constructor()
    {
        // Arrange

        // Act
        var response = new ZitidarError(1, "Test Error");

        // Assert
        response.Should().NotBeNull();
    }

    [Test]
    [TestCase("message")]
    public void Constructor_GivenNullParameterValue_ShouldThrowArgumentNullException(string parameterName)
    {
        // Arrange
            
        // Act
        ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<ZitidarError>(parameterName);

        // Assert
    }

    [Test]
    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-10)]
    public void Constructor_GivenInvalidErrorCode_ShouldThrowArgumentOutOfRangeException(int invalidErrorCode)
    {
        // Arrange
            
        // Act
        ConstructorTestHelper.ValidateExceptionIsThrownIfParameterIsNull<ZitidarError, ArgumentOutOfRangeException>("errorCode", 
                                                                                                                    false, ("errorCode", invalidErrorCode));

        // Assert
    }

    [Test]
    [TestCase("errorCode", "ErrorCode")]
    [TestCase("message", "Message")]
    [TestCase("exception", "Exception")]
    public void Constructor_GivenParameterValue_ShouldSetPropertyValue(string parameterName, string propertyName)
    {
        // Arrange

        // Act
        ConstructorTestHelper.ValidatePropertySetWithParameter<ZitidarError>(parameterName, propertyName);

        // Assert
    }
}