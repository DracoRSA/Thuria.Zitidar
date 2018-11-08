using System.Collections.Generic;
using System.IO;
using System.Linq;
using NSubstitute;
using NUnit.Framework;

using Thuria.Zitidar.Core;
using Thuria.Calot.TestUtilities;

namespace Thuria.Zitidar.Serialization.Nancy.Tests
{
  [TestFixture]
  public class TestNancyJsonSerializer
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var serializerSettings = Substitute.For<IThuriaSerializerSettings>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var jsonSerializer = new ThuriaNancyJsonSerializer(serializerSettings);
      //---------------Test Result -----------------------
      Assert.IsNotNull(jsonSerializer);
    }

    [TestCase("serializerSettings")]
    public void Constructor_GivenNullParameter_ShouldThrowException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<ThuriaNancyJsonSerializer>(parameterName);
      //---------------Test Result -----------------------
    }

    [Test]
    public void Constructor_ShouldNotInitialiseExtensions()
    {
      //---------------Set up test pack-------------------
      var serializerSettings = Substitute.For<IThuriaSerializerSettings>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var jsonSerializer = new ThuriaNancyJsonSerializer(serializerSettings);
      //---------------Test Result -----------------------
      Assert.IsNull(jsonSerializer.Extensions);
    }

    [Test]
    public void CanSerialize_GivenJsonType_ShouldReturnTrue()
    {
      //---------------Set up test pack-------------------
      var jsonSerializer = new ThuriaNancyJsonSerializer(new ThuriaDefaultJsonSerializerSettings());
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var canSerialize = jsonSerializer.CanSerialize("application/json");
      //---------------Test Result -----------------------
      Assert.IsTrue(canSerialize);
    }

    [Test]
    public void CanSerialize_GivenNonJsonType_ShouldReturnTrue()
    {
      //---------------Set up test pack-------------------
      var jsonSerializer = new ThuriaNancyJsonSerializer(new ThuriaDefaultJsonSerializerSettings());
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var canSerialize = jsonSerializer.CanSerialize("application/mime");
      //---------------Test Result -----------------------
      Assert.IsFalse(canSerialize);
    }

    [Test]
    public void CanDeserialize_GivenJsonType_ShouldReturnTrue()
    {
      //---------------Set up test pack-------------------
      var jsonSerializer = new ThuriaNancyJsonSerializer(new ThuriaDefaultJsonSerializerSettings());
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var canSerialize = jsonSerializer.CanDeserialize("application/json", null);
      //---------------Test Result -----------------------
      Assert.IsTrue(canSerialize);
    }

    [Test]
    public void CanDeserialize_GivenNonJsonType_ShouldReturnTrue()
    {
      //---------------Set up test pack-------------------
      var jsonSerializer = new ThuriaNancyJsonSerializer(new ThuriaDefaultJsonSerializerSettings());
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var canSerialize = jsonSerializer.CanDeserialize("application/mime", null);
      //---------------Test Result -----------------------
      Assert.IsFalse(canSerialize);
    }

    [Test]
    public void Serialize_GivenObject_ShouldReturnSerializedData()
    {
      //---------------Set up test pack-------------------
      var jsonSerializer = new ThuriaNancyJsonSerializer(new ThuriaDefaultJsonSerializerSettings());
      var testClass      = new FakeTestClass();
      //---------------Assert Precondition----------------

      //---------------Execute Test ----------------------
      using (var outputStream = new MemoryStream())
      {
        jsonSerializer.Serialize("application/json", testClass, outputStream);
        //---------------Test Result -----------------------
        Assert.Greater(outputStream.Length, 0);
      }
    }

    [Test]
    public void Deserialize_GivenSerializedData_ShouldReturnInstanceOfModel()
    {
      //---------------Set up test pack-------------------
      var jsonSerializer = new ThuriaNancyJsonSerializer(new ThuriaDefaultJsonSerializerSettings());
      var testClass = new FakeTestClass
                        {
                          IsTested = true,
                          Messages = new List<string> {"Test1", "Test2"}
                        };

      var jsonData = jsonSerializer.SerializeObject(testClass);

      using (var memoryStream = new MemoryStream())
      {
        using (var streamWriter = new StreamWriter(memoryStream))
        {
          streamWriter.Write(jsonData);
          streamWriter.Flush();
          memoryStream.Position = 0;

          //---------------Assert Precondition----------------
          //---------------Execute Test ----------------------
          var returnedObject = (FakeTestClass) jsonSerializer.Deserialize("application/json", memoryStream, null);
          //---------------Test Result -----------------------
          Assert.IsNotNull(returnedObject);
          Assert.AreEqual(testClass.IsTested, returnedObject.IsTested);
          Assert.AreEqual(testClass.Messages.Count(), returnedObject.Messages.Count());
          CollectionAssert.AreEquivalent(testClass.Messages, returnedObject.Messages);
        }
      }
    }
  }
}
