using System.Linq;
using System.Collections.Generic;

using NUnit.Framework;

namespace Thuria.Zitidar.Extensions.Tests
{
  [TestFixture]
  public class TestEnumerableExtensions
  {
    [Test]
    public void ForEach_GivenListAndActions_ShouldExecuteActionsForAllItems()
    {
      //---------------Set up test pack-------------------
      var testList   = new List<string> { "Test1", "Test2", "Test3" };
      var resultList = new List<string>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      testList.ForEach(item => resultList.Add(item));
      //---------------Test Result -----------------------
      Assert.IsTrue(resultList.Any());
      CollectionAssert.AreEqual(testList, resultList);
    }

    [Test]
    public void IsEmpty_GivenEmptyList_ShouldReturnTrue()
    {
      //---------------Set up test pack-------------------
      var testList = new List<object>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var result = testList.IsEmpty();
      //---------------Test Result -----------------------
      Assert.IsTrue(result);
    }

    [Test]
    public void IsEmpty_GivenListWithItems_ShouldReturnFalse()
    {
      //---------------Set up test pack-------------------
      var testList = new List<string> { "Test 1", "Test 2" };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var result = testList.IsEmpty();
      //---------------Test Result -----------------------
      Assert.IsFalse(result);
    }

    [Test]
    public void GetAllAsString_GivenListOfObjects_ShouldReturnStringRepresentationOfAll()
    {
      //---------------Set up test pack-------------------
      var fakeObject1 = new FakeObject("Test1");
      var fakeObject2 = new FakeObject("Test2");
      var testList    = new List<object> { fakeObject1, fakeObject2 };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var toStringResult = testList.GetAllAsString();
      //---------------Test Result -----------------------
      Assert.IsFalse(string.IsNullOrWhiteSpace(toStringResult));
      StringAssert.Contains(fakeObject1.ToString(), toStringResult);
      StringAssert.Contains(fakeObject2.ToString(), toStringResult);
    }

    [Test]
    public void And_GivenListWithItems_ShouldReturnArrayWithItemsAdded()
    {
      //---------------Set up test pack-------------------
      var testList = new List<string> { "Test 1", "Test 2" };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var result = testList.And("Test 3", "Test 4");
      //---------------Test Result -----------------------
      Assert.AreEqual(4, result.Count());
      Assert.IsTrue(result.Contains("Test 1"));
      Assert.IsTrue(result.Contains("Test 2"));
      Assert.IsTrue(result.Contains("Test 3"));
      Assert.IsTrue(result.Contains("Test 4"));
    }

    private class FakeObject
    {
      public bool WasCalled { get; set; } = false;

      private string toStringMessage = typeof(FakeObject).ToString();

      public FakeObject()
      {
      }

      public FakeObject(string toStringMessage)
      {
        this.toStringMessage = toStringMessage;
      }
    }
  }
}
