using System;

using NUnit.Framework;

namespace Thuria.Zitidar.Extensions.Tests
{
  [TestFixture]
  public class TestTypeExtensions
  {
    [Test]
    public void GetPropertyValue_GivenPropertyDoesNotExist_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      var fakeReflection = new FakeReflection();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<ArgumentException>(() => fakeReflection.GetPropertyValue("InvalidProperty"));
      //---------------Test Result -----------------------
      StringAssert.Contains("Property [InvalidProperty] does not exist on object", exception.Message);
    }

    [Test]
    public void GetPropertyValue_GivenPropertyExists_ShouldReturnPropertyValue()
    {
      //---------------Set up test pack-------------------
      var newTestValue   = "Test Value";
      var fakeReflection = new FakeReflection
        {
          ValidProperty = newTestValue
        };
      //---------------Assert Precondition----------------
      Assert.AreEqual(newTestValue, fakeReflection.ValidProperty);
      //---------------Execute Test ----------------------
      var result = fakeReflection.GetPropertyValue("ValidProperty");
      //---------------Test Result -----------------------
      Assert.AreEqual(newTestValue, result);
    }

    [Test]
    public void SetPropertyValue_GivenPropertyDoesNotExist_ShouldThrowException()
    {
      //---------------Set up test pack-------------------
      var fakeReflection = new FakeReflection();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<ArgumentException>(() => fakeReflection.SetPropertyValue("InvalidProperty", "TestValue"));
      //---------------Test Result -----------------------
      StringAssert.Contains("Property [InvalidProperty] does not exist on object", exception.Message);
    }

    [Test]
    public void SetPropertyValue_GivenPropertyExists_ShouldSetPropertyValue()
    {
      //---------------Set up test pack-------------------
      var oldTestValue   = "Old Value";
      var newTestValue   = "Test Value";
      var fakeReflection = new FakeReflection
        {
          ValidProperty = oldTestValue
        };
      //---------------Assert Precondition----------------
      Assert.AreEqual(oldTestValue, fakeReflection.ValidProperty);
      //---------------Execute Test ----------------------
      fakeReflection.SetPropertyValue("ValidProperty", newTestValue);
      //---------------Test Result -----------------------
      Assert.AreEqual(newTestValue, fakeReflection.ValidProperty);
    }

    [Test]
    public void DoesPropertyExist_GivenPropertyExists_ShouldReturnTrue()
    {
      //---------------Set up test pack-------------------
      var fakeReflection = new FakeReflection();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var returnValue = fakeReflection.DoesPropertyExist("ValidProperty");
      //---------------Test Result -----------------------
      Assert.IsTrue(returnValue);
    }

    [Test]
    public void DoesPropertyExist_GivenPropertyDoesNotExist_ShouldReturnFalse()
    {
      //---------------Set up test pack-------------------
      var fakeReflection = new FakeReflection();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var returnValue = fakeReflection.DoesPropertyExist("InvalidProperty");
      //---------------Test Result -----------------------
      Assert.IsFalse(returnValue);
    }
  }
}
