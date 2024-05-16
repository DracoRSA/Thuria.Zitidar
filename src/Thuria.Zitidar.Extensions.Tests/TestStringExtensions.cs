using NUnit.Framework;
using FluentAssertions;

namespace Thuria.Zitidar.Extensions.Tests;

[TestFixture]
public class TestStringExtensions
{
    [TestCase("Test Case 1", "testCase1")]
    [TestCase("test Case 1", "testCase1")]
    [TestCase("Test case 1", "testCase1")]
    [TestCase("Testcase 1", "testcase1")]
    [TestCase("Test_Case%1", "testCase1")]
    [TestCase("Test   Case    1", "testCase1")]
    [TestCase("Test__Case___1", "testCase1")]
    public void CamelCase_GivenString_ShouldPascalCaseString(string inputString, string expectedResult)
    {
        //---------------Set up test pack-------------------
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        var result = inputString.CamelCase();
        //---------------Test Result -----------------------
        result.Should().Be(expectedResult);
    }

    [TestCase("Test Case 1", "TestCase1")]
    [TestCase("test Case 1", "TestCase1")]
    [TestCase("Test case 1", "TestCase1")]
    [TestCase("Testcase 1", "Testcase1")]
    [TestCase("Test_Case%1", "TestCase1")]
    [TestCase("Test   Case    1", "TestCase1")]
    [TestCase("Test__Case___1", "TestCase1")]
    public void PascalCase_GivenString_ShouldPascalCaseString(string inputString, string expectedResult)
    {
        //---------------Set up test pack-------------------
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        var result = inputString.PascalCase();
        //---------------Test Result -----------------------
        result.Should().Be(expectedResult);
    }

    [Test]
    public void EscapeQuotes_GivenString_ShouldEscapeQuotes()
    {
        //---------------Set up test pack-------------------
        var inputString = "Test \" Some string";
        //---------------Assert Precondition----------------
        //---------------Execute Test ----------------------
        var result = inputString.EscapeQuotes();
        //---------------Test Result -----------------------
        result.Should().Be(@"Test \"" Some string");
    }
}