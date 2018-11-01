using NSubstitute;
using NUnit.Framework;
using FluentAssertions;

using Thuria.Zitidar.Core;

namespace Thuria.Zitidar.Nancy.Tests
{
  [TestFixture]
  public class TestNancyServiceHost
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var iocContainer = Substitute.For<IThuriaIocContainer>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var serviceHost = new NancyServiceHost(iocContainer);
      //---------------Test Result -----------------------
      serviceHost.Should().NotBeNull();
    }

    [Test]
    public void Constructor_GivenContainer_ShouldUseGivenContainerAndNotLoadContainerFromNancyStartup()
    {
      //---------------Set up test pack-------------------
      var iocContainer             = Substitute.For<IThuriaIocContainer>();
      NancyStartup.IocContainer    = null;
      NancyServiceHost serviceHost = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => serviceHost = new NancyServiceHost(iocContainer));
      //---------------Test Result -----------------------
      serviceHost.Should().NotBeNull();
      iocContainer.Received(1).GetInstance<IThuriaNancySettings>();
      iocContainer.Received(1).GetInstance<IThuriaLogger>();
    }

    [Test]
    public void Constructor_GivenNullContainer_ShouldLoadContainerFromNancyStartup()
    {
      //---------------Set up test pack-------------------
      var iocContainer             = Substitute.For<IThuriaIocContainer>();
      NancyStartup.IocContainer    = iocContainer;
      NancyServiceHost serviceHost = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => serviceHost = new NancyServiceHost(null));
      //---------------Test Result -----------------------
      serviceHost.Should().NotBeNull();
      iocContainer.Received(1).GetInstance<IThuriaNancySettings>();
      iocContainer.Received(1).GetInstance<IThuriaLogger>();
    }
  }
}
