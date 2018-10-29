using NUnit.Framework;
using Thuria.Zitidar.Core;

namespace Thuria.Zitidar.Settings.Tests
{
  [TestFixture]
  public class TestSettings
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var thuriaSettings = new ThuriaSettings();
      //---------------Test Result -----------------------
      Assert.IsNotNull(thuriaSettings);
    }

    [Test]
    public void ApplicationName_GivenApplicationNameExists_ShouldReturnValue()
    {
      //---------------Set up test pack-------------------
      var expectedApplicationName = "SettingsTests";
      var thuriaSettings          = new ThuriaSettings();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var settingsApplicationName = thuriaSettings.ApplicationName;
      //---------------Test Result -----------------------
      Assert.That(settingsApplicationName, Is.EqualTo(expectedApplicationName));
    }

    [Test]
    public void ApplicationName_GivenStartDebugMode_ShouldReturnValue()
    {
      //---------------Set up test pack-------------------
      var thuriaSettings = new ThuriaSettings();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var settingsValue = thuriaSettings.StartDebugMode;
      //---------------Test Result -----------------------
      Assert.That(settingsValue, Is.False);
    }

    [Test]
    public void EnvironmentSettingsType_GivenVariableExists_ShouldReturnValue()
    {
      //---------------Set up test pack-------------------
      var enviromentTestSettings = new EnviromentTestSettings()
        {
          SettingsType = ThuriaSettingsType.Environment
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var pathReturnValue = enviromentTestSettings.Path;
      //---------------Test Result -----------------------
      Assert.That(pathReturnValue, Is.Not.Null);
    }

    private class EnviromentTestSettings : ThuriaSettings
    {
      public string Path => this.GetConfigurationValue<string>("Path", true);
    }
  }
}
