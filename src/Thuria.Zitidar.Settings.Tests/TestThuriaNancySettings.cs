using NUnit.Framework;

namespace Thuria.Zitidar.Settings.Tests
{
  [TestFixture]
  public class TestThuriaNancySettings
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var nancySettings = new ThuriaNancySettings();
      //---------------Test Result -----------------------
      Assert.IsNotNull(nancySettings);
    }

    [Test]
    public void Origin_GivenHostBaseUriExists_ShouldReturnValue()
    {
      //---------------Set up test pack-------------------
      var expectedHostBaseUri = "http://localhost:21575";
      var nancySettings       = new ThuriaNancySettings();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var hostBaseUri = nancySettings.HostBaseUri;
      //---------------Test Result -----------------------
      Assert.That(hostBaseUri, Is.EqualTo(expectedHostBaseUri));
    }
  }
}
