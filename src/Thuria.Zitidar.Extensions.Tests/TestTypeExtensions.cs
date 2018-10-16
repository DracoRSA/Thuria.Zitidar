using System;
using System.Collections.Generic;

using NUnit.Framework;
using FluentAssertions;

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

    [TestCase(typeof(string), default(string))]
    [TestCase(typeof(int), default(int))]
    [TestCase(typeof(uint), default(uint))]
    [TestCase(typeof(long), default(long))]
    [TestCase(typeof(float), default(float))]
    [TestCase(typeof(bool), default(bool))]
    public void GetDefaultData_ShouldReturnExpectedDefaultData(Type objectType, object expectedData)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var defaultData = objectType.GetDefaultData();
      //---------------Test Result -----------------------
      defaultData.Should().Be(expectedData);
    }

    [Test]
    public void GetDefaultData_GivenGuid_ShouldReturnEmptyGuid()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var defaultData = typeof(Guid).GetDefaultData();
      //---------------Test Result -----------------------
      defaultData.Should().Be(Guid.Empty);
    }

    [Test]
    public void GetDefaultData_GivenDateTime_ShouldReturnMinDate()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var defaultData = typeof(DateTime).GetDefaultData();
      //---------------Test Result -----------------------
      defaultData.Should().Be(DateTime.MinValue);
    }

    [Test]
    public void GetDefaultData_GivenDecimal_ShouldReturnDefaultDecimalValue()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var defaultData = typeof(decimal).GetDefaultData();
      //---------------Test Result -----------------------
      defaultData.Should().Be(default(decimal));
    }

    [Test]
    public void GetDefaultData_GivenIList_ShouldReturnEmptyList()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var defaultData = typeof(IList<FakeReflection>).GetDefaultData();
      //---------------Test Result -----------------------
      defaultData.Should().NotBeNull();
      defaultData.Should().BeAssignableTo<IList<FakeReflection>>();
    }
  }
}
