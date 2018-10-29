using System;
using NUnit.Framework;

namespace Thuria.Zitidar.Settings.Tests
{
  [TestFixture]
  public class TestDatabaseSettings
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var databaseSettings = new ThuriaDatabaseSettings();
      //---------------Test Result -----------------------
      Assert.IsNotNull(databaseSettings);
    }

    [Test]
    public void GetConnectionString_GivenContextExists_ShouldReturnConnectionString()
    {
      //---------------Set up test pack-------------------
      var databaseSettings = new ThuriaDatabaseSettings();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var connectionString = databaseSettings.GetConnectionString("TestDB");
      //---------------Test Result -----------------------
      Assert.That(connectionString, Is.Not.Empty);
    }

    [Test]
    public void GetConnectionString_GivenContextDoesNotExist_ShouldReturnThrowException()
    {
      //---------------Set up test pack-------------------
      var databaseSettings = new ThuriaDatabaseSettings();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.Throws<Exception>(() => databaseSettings.GetConnectionString("Unknown"));
      //---------------Test Result -----------------------
      StringAssert.Contains("ConnectionStrings:Unknown not found in Application Configuration", exception.Message);
    }
  }
}
