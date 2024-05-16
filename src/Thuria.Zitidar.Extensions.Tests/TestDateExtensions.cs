using System;
using FluentAssertions;
using NUnit.Framework;

namespace Thuria.Zitidar.Extensions.Tests;

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

        startOfDay.Year.Should().Be(inputValue.Year);
        startOfDay.Month.Should().Be(inputValue.Month);
        startOfDay.Day.Should().Be(inputValue.Day);
        startOfDay.Hour.Should().Be(0);
        startOfDay.Minute.Should().Be(0);
        startOfDay.Second.Should().Be(0);
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
        endOfDay.Year.Should().Be(inputValue.Year);
        endOfDay.Month.Should().Be(inputValue.Month);
        endOfDay.Day.Should().Be(inputValue.Day);
        endOfDay.Hour.Should().Be(23);
        endOfDay.Minute.Should().Be(59);
        endOfDay.Second.Should().Be(59);
    }
}