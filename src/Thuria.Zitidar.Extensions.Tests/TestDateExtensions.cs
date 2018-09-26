using System;
using NUnit.Framework;

namespace Thuria.Zitidar.Extensions.Tests
{
  [TestFixture]
  public class TestDateExtensions
  {
    [Test]
    public void StartOfDay_GivenSpecificDate_ShouldReturnExpectedDateTime()
    {
      //---------------Set up test pack-------------------
      var inputValue = new DateTime(2015, 6, 15, 18, 23, 30);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var startOfDay = inputValue.StartOfDay();
      //---------------Test Result -----------------------
      Assert.AreEqual(inputValue.Year, startOfDay.Year);
      Assert.AreEqual(inputValue.Month, startOfDay.Month);
      Assert.AreEqual(inputValue.Day, startOfDay.Day);
      Assert.AreEqual(0, startOfDay.Hour);
      Assert.AreEqual(0, startOfDay.Minute);
      Assert.AreEqual(0, startOfDay.Second);
    }

    [Test]
    public void EndOfDay_GivenSpecificDate_ShouldReturnExpectedDateTime()
    {
      //---------------Set up test pack-------------------
      var inputValue = new DateTime(2015, 6, 15, 18, 23, 30);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var endOfDay = inputValue.EndOfDay();
      //---------------Test Result -----------------------
      Assert.AreEqual(inputValue.Year, endOfDay.Year);
      Assert.AreEqual(inputValue.Month, endOfDay.Month);
      Assert.AreEqual(inputValue.Day, endOfDay.Day);
      Assert.AreEqual(23, endOfDay.Hour);
      Assert.AreEqual(59, endOfDay.Minute);
      Assert.AreEqual(59, endOfDay.Second);
    }
  }
}
