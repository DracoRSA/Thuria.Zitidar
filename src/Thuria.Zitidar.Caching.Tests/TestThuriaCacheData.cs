using System;

using NUnit.Framework;
using FluentAssertions;

using Thuria.Calot.TestUtilities;

namespace Thuria.Zitidar.Caching.Tests
{
  [TestFixture]
  public class TestThuriaCacheData
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var cacheData = new ThuriaCacheData<string>("Test", DateTime.UtcNow.AddSeconds(5));
      //---------------Test Result -----------------------
      cacheData.Should().NotBeNull();
    }

    [TestCase("cacheValue")]
    public void Constructor_GivenNullParameter_ShouldThrowArgumentNullException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<ThuriaCacheData<string>>(parameterName);
      //---------------Test Result -----------------------
    }

    [TestCase("Expiry")]
    public void Property_GivenValue_ShouldSetPropertyValue(string propertyName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      PropertyTestHelper.ValidateGetAndSet<ThuriaCacheData<string>>(propertyName);
      //---------------Test Result -----------------------
    }
  }
}
