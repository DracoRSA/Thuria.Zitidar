using System.Linq;
using System.Collections.Generic;

using NUnit.Framework;
using FluentAssertions;

namespace Thuria.Zitidar.Extensions.Tests;

[TestFixture]
public class TestEnumerableExtensions
{
    [Test]
    public void ForEach_GivenListAndActions_ShouldExecuteActionsForAllItems()
    {
        //---------------Set up test pack-------------------
        var testList = new List<string> { "Test1", "Test2", "Test3" };
        var resultList = new List<string>();
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        testList.ForEach(item => resultList.Add(item));
        //---------------Test Result -----------------------
        resultList.Any().Should().BeTrue();
        resultList.Should().BeEquivalentTo(testList);
    }

    [Test]
    public void IsEmpty_GivenEmptyList_ShouldReturnTrue()
    {
        //---------------Set up test pack-------------------
        // ReSharper disable once CollectionNeverUpdated.Local
        var testList = new List<object>();
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        var result = testList.IsEmpty();
        //---------------Test Result -----------------------
        result.Should().BeTrue();
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
        result.Should().BeFalse();
    }

    [Test]
    public void GetAllAsString_GivenListOfObjects_ShouldReturnStringRepresentationOfAll()
    {
        //---------------Set up test pack-------------------
        var fakeObject1 = new FakeObject("Test1");
        var fakeObject2 = new FakeObject("Test2");
        var testList = new List<object> { fakeObject1, fakeObject2 };
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        var toStringResult = testList.GetAllAsString();
        //---------------Test Result -----------------------
        toStringResult.Should().NotBeNullOrWhiteSpace();
        toStringResult.Should().Contain(fakeObject1.ToString());
        toStringResult.Should().Contain(fakeObject2.ToString());
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
        var resultList = result as string[] ?? result.ToArray();
        resultList.Length.Should().Be(4);
        resultList.Should().Contain("Test 1");
        resultList.Should().Contain("Test 2");
        resultList.Should().Contain("Test 3");
        resultList.Should().Contain("Test 4");
    }

    private class FakeObject
    {
        // ReSharper disable once NotAccessedField.Local
        private string _toStringMessage = typeof(FakeObject).ToString();

        // ReSharper disable once UnusedMember.Local
        public FakeObject()
        {
        }

        // ReSharper disable once UnusedMember.Local
        public bool WasCalled { get; set; } = false;

        public FakeObject(string toStringMessage)
        {
            _toStringMessage = toStringMessage;
        }
    }
}