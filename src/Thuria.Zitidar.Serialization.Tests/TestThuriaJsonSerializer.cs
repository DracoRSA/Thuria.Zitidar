using System;

using NSubstitute;
using NUnit.Framework;
using FluentAssertions;

using Thuria.Zitidar.Core;
using Thuria.Calot.TestUtilities;

namespace Thuria.Zitidar.Serialization.Tests
{
  [TestFixture]
  public class TestThuriaJsonSerializer
  {
    private string _serializedJsonData =
      "{\"$id\":\"1\",\"_name\":\"Thuria.Zitidar.Serialization.Tests.FakeSerializationClass, Thuria.Zitidar.Serialization.Tests\",\"Id\":\"e4df5e95-c956-409c-b9f1-a0a5a8505303\",\"TestName\":\"TestName\"}";

    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var serializerSettings = Substitute.For<IThuriaSerializerSettings>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var jsonSerializer = new ThuriaJsonSerializer(serializerSettings);
      //---------------Test Result -----------------------
      jsonSerializer.Should().NotBeNull();
    }

    [TestCase("serializerSettings")]
    public void Constructor_GivenNullParameter_ShouldThrowException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<ThuriaJsonSerializer>(parameterName);
      //---------------Test Result -----------------------
    }

    [Test]
    public void SerializeObject_GivenNullObject_ShouldReturnNull()
    {
      //---------------Set up test pack-------------------
      var jsonSerializer = new ThuriaJsonSerializer(new ThuriaDefaultJsonSerializerSettings());
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var jsonString = jsonSerializer.SerializeObject<FakeSerializationClass>(null);
      //---------------Test Result -----------------------
      Assert.IsTrue(string.IsNullOrWhiteSpace(jsonString));
    }

    [Test]
    public void SerializeObject_GivenValidObject_ShouldReturnJsonString()
    {
      //---------------Set up test pack-------------------
      var serializationClass = new FakeSerializationClass();
      var jsonSerializer = new ThuriaJsonSerializer(new ThuriaDefaultJsonSerializerSettings());
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var jsonString = jsonSerializer.SerializeObject(serializationClass);
      //---------------Test Result -----------------------
      Assert.IsNotNull(jsonString);
      Assert.IsFalse(string.IsNullOrWhiteSpace(jsonString));
      Assert.AreEqual(_serializedJsonData, jsonString);
    }

    [Test]
    public void DeserializeObject_Generic_GivenInvalidJsonString_ShouldReturnNull()
    {
      //---------------Set up test pack-------------------
      var jsonSerializer = new ThuriaJsonSerializer(new ThuriaDefaultJsonSerializerSettings());
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var serializationClass = jsonSerializer.DeserializeObject<FakeSerializationClass>("TestData");
      //---------------Test Result -----------------------
      Assert.IsNull(serializationClass);
    }

    [Test]
    public void DeserializeObject_Generic_GivenJsonString_ShouldReturnValidObject()
    {
      //---------------Set up test pack-------------------
      var jsonSerializer = new ThuriaJsonSerializer(new ThuriaDefaultJsonSerializerSettings());
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var serializationClass = jsonSerializer.DeserializeObject<FakeSerializationClass>(_serializedJsonData, true);
      //---------------Test Result -----------------------
      Assert.IsNotNull(serializationClass);
      Assert.IsInstanceOf<FakeSerializationClass>(serializationClass);
      Assert.AreEqual(new Guid("e4df5e95-c956-409c-b9f1-a0a5a8505303"), serializationClass.Id);
      Assert.AreEqual("TestName", serializationClass.TestName);
    }

    [Test]
    public void DeserializeObject_GivenInvalidJsonString_ShouldReturnNull()
    {
      //---------------Set up test pack-------------------
      var jsonSerializer = new ThuriaJsonSerializer(new ThuriaDefaultJsonSerializerSettings());
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var serializationClass = jsonSerializer.DeserializeObject("TestData");
      //---------------Test Result -----------------------
      Assert.IsNull(serializationClass);
    }

    [Test]
    public void DeserializeObject_GivenJsonString_ShouldReturnValidObject()
    {
      //---------------Set up test pack-------------------
      var jsonSerializer = new ThuriaJsonSerializer(new ThuriaDefaultJsonSerializerSettings());
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var serializationClass = jsonSerializer.DeserializeObject(_serializedJsonData, true) as FakeSerializationClass;
      //---------------Test Result -----------------------
      Assert.IsNotNull(serializationClass);
      Assert.IsInstanceOf<FakeSerializationClass>(serializationClass);
      Assert.AreEqual(new Guid("e4df5e95-c956-409c-b9f1-a0a5a8505303"), serializationClass.Id);
      Assert.AreEqual("TestName", serializationClass.TestName);
    }

    [Test]
    public void SerializeObject_ShouldSerializeToJsonData()
    {
      //---------------Set up test pack-------------------
      var fakeTestData = new FakeTestData
                           {
                             ItemPrices = new decimal[] {1.0m, 2.3m, 4.81m}
                           };
      var jsonSerializer = new ThuriaJsonSerializer(new ThuriaDefaultJsonSerializerSettings());
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var jsonData = jsonSerializer.SerializeObject(fakeTestData);
      //---------------Test Result -----------------------
      Console.WriteLine(jsonData);
      Assert.That(jsonData, Is.EqualTo(SerializationTestData.SerializedJsonData));
    }

    [Test]
    public void DeserializeObject_GivenSerializedJsonData_ShouldDeserializeToObject()
    {
      //---------------Set up test pack-------------------
      var fakeTestData = new FakeTestData
                           {
                             ItemPrices = new decimal[] {1.0m, 2.0m, 3.12345m}
                           };
      var jsonSerializer = new ThuriaJsonSerializer(new ThuriaDefaultJsonSerializerSettings());
      var jsonData = jsonSerializer.SerializeObject(fakeTestData);
      Console.WriteLine(jsonData);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var deserializeObject = jsonSerializer.DeserializeObject(jsonData);
      //---------------Test Result -----------------------
      deserializeObject.Should().BeEquivalentTo(fakeTestData);
    }

    [Test]
    public void DeserializeObject_GivenDataSerializedInCore_ShouldReturnDeserializedObject()
    {
      //---------------Set up test pack-------------------
      var jsonData = SerializationTestData.SerializedJsonData;
      var jsonSerializer = new ThuriaJsonSerializer(new ThuriaDefaultJsonSerializerSettings());
      FakeTestData fakeTestData = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => fakeTestData = jsonSerializer.DeserializeObject<FakeTestData>(jsonData, true));
      //---------------Test Result -----------------------
      Assert.IsNotNull(fakeTestData);
    }
  }
}