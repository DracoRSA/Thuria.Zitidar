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
      var cacheData = new ThuriaCacheData<string>(DateTime.UtcNow.AddSeconds(5), "Test");
      //---------------Test Result -----------------------
      cacheData.Should().NotBeNull();
    }

    [TestCase("expiryValue")]
    public void Constructor_GivenNullParameter_ShouldThrowArgumentNullException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<ThuriaCacheData<string>>(parameterName);
      //---------------Test Result -----------------------
    }
  }
}
