using NUnit.Framework;
using FluentAssertions;

using Thuria.Zitidar.Core;

namespace Thuria.Zitidar.Structuremap.Tests
{
  [TestFixture]
  public class TestStructuremapThuriaBootstrapper
  {
    [Test]
    public void Create_ShouldReturnNewlyCreatedBootstrapper()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var bootstrapper = StructuremapThuriaBootstrapper.Create();
      //---------------Test Result -----------------------
      bootstrapper.Should().NotBeNull();
      bootstrapper.Should().BeAssignableTo<IThuriaBootstrapper>();
      bootstrapper.Should().BeAssignableTo<IStructuremapThuriaBootstrapper>();
      bootstrapper.Should().BeAssignableTo<StructuremapThuriaBootstrapper>();
    }

    [TestCase(true)]
    [TestCase(false)]
    public void WithScanningOfFiles_GivenIndicator_ShouldSetScanFilesIndicator(bool scanIndicator)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var bootstrapper = FakeTestBootstrapper.Create()
                                             .WithScannningOfFiles(scanIndicator)
                                             .Build();
      //---------------Test Result -----------------------
      Assert.AreEqual(scanIndicator, ((FakeTestBootstrapper)bootstrapper).IsScanningFiles);
    }
    
    [Test]
    public void WithRegistry_GivenRegistry_ShouldAddRegistryToRegistryCollection()
    {
      //---------------Set up test pack-------------------
      var fakeRegistry = new FakeRegistry();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var bootstrapper = StructuremapThuriaBootstrapper.Create()
                                                       .WithRegistry(fakeRegistry)
                                                       .Build();
      //---------------Test Result -----------------------
      var fakeClass = bootstrapper.IocContainer.GetInstance<IFakeInterface>();
      Assert.IsNotNull(fakeClass);
      Assert.IsInstanceOf<FakeClass>(fakeClass);
    }
    
    [Test]
    public void WithObjectMapping_GivenMappings_ShouldAddMappingsToContainer()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var bootstrapper = StructuremapThuriaBootstrapper.Create()
                                                       .WithObjectMapping(typeof(IFakeInterface), typeof(FakeClass))
                                                       .Build();
      //---------------Test Result -----------------------
      var fakeClass = bootstrapper.IocContainer.GetInstance<IFakeInterface>();
      fakeClass.Should().NotBeNull();
      fakeClass.Should().BeAssignableTo<IFakeInterface>();
    }
    
    [Test]
    public void WithConcreteObjectMapping_GivenTypeAndConcreteObjectMapping_ShouldAddMappingToContainer()
    {
      //---------------Set up test pack-------------------
      var fakeClass = new FakeClass();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var bootstrapper = StructuremapThuriaBootstrapper.Create()
                                                       .WithConcreteObjectMapping(typeof(IFakeInterface), fakeClass)
                                                       .Build();
      //---------------Test Result -----------------------
      var foundClass = bootstrapper.IocContainer.GetInstance<IFakeInterface>();
      foundClass.Should().NotBeNull();
      foundClass.Should().BeAssignableTo<IFakeInterface>();
      foundClass.Should().BeAssignableTo<FakeClass>();
    }
    
    [Test]
    public void Initialise_ShouldReturnBootstrapper()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var bootstrapper = StructuremapThuriaBootstrapper.Create().Build();
      //---------------Test Result -----------------------
      bootstrapper.Should().NotBeNull();
      bootstrapper.Should().BeAssignableTo<IThuriaBootstrapper>();
      bootstrapper.Should().BeAssignableTo<IStructuremapThuriaBootstrapper>();
      bootstrapper.Should().BeAssignableTo<StructuremapThuriaBootstrapper>();
    }
  }
}
