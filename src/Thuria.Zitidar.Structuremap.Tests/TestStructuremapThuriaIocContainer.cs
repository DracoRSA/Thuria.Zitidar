using System.Linq;
using System.Collections.Generic;

using Moq;
using NUnit.Framework;
using FluentAssertions;
using NSubstitute;
using Thuria.Calot.TestUtilities;

using StructureMap;

namespace Thuria.Zitidar.Structuremap.Tests
{
  [TestFixture]
  public class TestStructuremapThuriaIocContainer
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var container = new Mock<IContainer>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var iocContainer = new StructuremapThuriaIocContainer(container.Object);
      //---------------Test Result -----------------------
      iocContainer.Should().NotBeNull();
    }
    
    [TestCase("iocContainer")]
    public void Constructor_GivenNullParameter_ShouldThrowArgumentNullException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<StructuremapThuriaIocContainer>(parameterName);
      //---------------Test Result -----------------------
    }
    
    [Test]
    public void Constructor_GivenContainer_ShouldSetContainerProperty()
    {
      //---------------Set up test pack-------------------
      var container    = Substitute.For<IContainer>();
      var iocContainer = new StructuremapThuriaIocContainer(container);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var iocContainerContainer = iocContainer.Container;
      //---------------Test Result -----------------------
      container.Should().Be(iocContainerContainer);
    }
    
    [Test]
    public void GetInstance_Generic_ShouldGetInstanceFromStructuremapContainer()
    {
      //---------------Set up test pack-------------------
      var container    = new Mock<IContainer>();
      var iocContainer = new StructuremapThuriaIocContainer(container.Object);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      iocContainer.GetInstance<IFakeObject>();
      //---------------Test Result -----------------------
      container.Verify(container1 => container1.TryGetInstance<IFakeObject>(), Times.Once);
    }
    
    [Test]
    public void GetInstance_Generic_GivenObjectNotFound_ShouldNotThrowExceptionAndReturnNull()
    {
      //---------------Set up test pack-------------------
      var container         = new Container();
      var iocContainer      = new StructuremapThuriaIocContainer(container);
      object returnedObject = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => returnedObject = iocContainer.GetInstance<IFakeObject>());
      //---------------Test Result -----------------------
      returnedObject.Should().BeNull();
    }
    
    [Test]
    public void GetInstance_Generic_GivenObjectExists_ShouldNotThrowExceptionAndReturnInstanceOfObject()
    {
      //---------------Set up test pack-------------------
      var container  = new Container();
      var fakeObject = new FakeObject();
      container.Inject(typeof(IFakeObject), fakeObject);
    
      var iocContainer      = new StructuremapThuriaIocContainer(container);
      object returnedObject = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => returnedObject = iocContainer.GetInstance<IFakeObject>());
      //---------------Test Result -----------------------
      returnedObject.Should().NotBeNull();
      returnedObject.Should().BeAssignableTo<IFakeObject>();
      returnedObject.Should().Be(fakeObject);
    }
    
    [Test]
    public void GetGenericInstance_Named_ShouldGetInstanceFromStructuremapContainer()
    {
      //---------------Set up test pack-------------------
      var container    = new Mock<IContainer>();
      var iocContainer = new StructuremapThuriaIocContainer(container.Object);
      var instanceName = "test";
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      iocContainer.GetInstance<IFakeObject>(instanceName);
      //---------------Test Result -----------------------
      container.Verify(container1 => container1.TryGetInstance<IFakeObject>(instanceName), Times.Once);
    }
    
    [Test]
    public void GetInstance_Generic_Named_GivenObjectNotFound_ShouldNotThrowExceptionAndReturnNull()
    {
      //---------------Set up test pack-------------------
      var container         = new Container();
      var iocContainer      = new StructuremapThuriaIocContainer(container);
      object returnedObject = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => returnedObject = iocContainer.GetInstance<IFakeObject>("test"));
      //---------------Test Result -----------------------
      returnedObject.Should().BeNull();
    }
    
    [Test]
    public void GetInstance_Generic_Named_GivenObjectExists_ShouldNotThrowExceptionAndReturnInstanceOfObject()
    {
      //---------------Set up test pack-------------------
      var instanceName = "test";
      var container    = new Container();
      container.Configure(expression =>
        {
          expression.For<IFakeObject>().Use<FakeObject>().Named(instanceName);
        });
    
      var iocContainer      = new StructuremapThuriaIocContainer(container);
      object returnedObject = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => returnedObject = iocContainer.GetInstance<IFakeObject>(instanceName));
      //---------------Test Result -----------------------
      returnedObject.Should().NotBeNull();
      returnedObject.Should().BeAssignableTo<IFakeObject>();
    }
    
    [Test]
    public void GetInstance_ShouldGetInstanceFromStructuremapContainer()
    {
      //---------------Set up test pack-------------------
      var container    = new Mock<IContainer>();
      var iocContainer = new StructuremapThuriaIocContainer(container.Object);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      iocContainer.GetInstance(typeof(IFakeObject));
      //---------------Test Result -----------------------
      container.Verify(container1 => container1.TryGetInstance(typeof(IFakeObject)), Times.Once);
    }
    
    [Test]
    public void GetInstance_GivenObjectNotFound_ShouldNotThrowExceptionAndReturnNull()
    {
      //---------------Set up test pack-------------------
      var container         = new Container();
      var iocContainer      = new StructuremapThuriaIocContainer(container);
      object returnedObject = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => returnedObject = iocContainer.GetInstance(typeof(IFakeObject)));
      //---------------Test Result -----------------------
      returnedObject.Should().BeNull();
    }
    
    [Test]
    public void GetInstance_GivenObjectExists_ShouldNotThrowExceptionAndReturnInstanceOfObject()
    {
      //---------------Set up test pack-------------------
      var container  = new Container();
      var fakeObject = new FakeObject();
      container.Inject(typeof(IFakeObject), fakeObject);
    
      var iocContainer      = new StructuremapThuriaIocContainer(container);
      object returnedObject = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => returnedObject = iocContainer.GetInstance(typeof(IFakeObject)));
      //---------------Test Result -----------------------
      returnedObject.Should().NotBeNull();
      returnedObject.Should().BeAssignableTo<IFakeObject>();
      returnedObject.Should().Be(fakeObject);
    }
    
    [Test]
    public void GetInstance_Named_ShouldGetInstanceFromStructuremapContainer()
    {
      //---------------Set up test pack-------------------
      var container    = new Mock<IContainer>();
      var iocContainer = new StructuremapThuriaIocContainer(container.Object);
      var instanceName = "test";
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      iocContainer.GetInstance(typeof(IFakeObject), instanceName);
      //---------------Test Result -----------------------
      container.Verify(container1 => container1.TryGetInstance(typeof(IFakeObject), instanceName), Times.Once);
    }
    
    [Test]
    public void GetInstance_Named_GivenObjectNotFound_ShouldNotThrowExceptionAndReturnNull()
    {
      //---------------Set up test pack-------------------
      var container         = new Container();
      var iocContainer      = new StructuremapThuriaIocContainer(container);
      object returnedObject = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => returnedObject = iocContainer.GetInstance(typeof(IFakeObject), "test"));
      //---------------Test Result -----------------------
      returnedObject.Should().BeNull();
    }
    
    [Test]
    public void GetInstance_Named_GivenObjectExists_ShouldNotThrowExceptionAndReturnInstanceOfObject()
    {
      //---------------Set up test pack-------------------
      var instanceName = "test";
      var container    = new Container();
      container.Configure(expression =>
        {
          expression.For<IFakeObject>().Use<FakeObject>().Named(instanceName);
        });
    
      var iocContainer      = new StructuremapThuriaIocContainer(container);
      object returnedObject = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => returnedObject = iocContainer.GetInstance(typeof(IFakeObject), instanceName));
      //---------------Test Result -----------------------
      returnedObject.Should().NotBeNull();
      returnedObject.Should().BeAssignableTo<IFakeObject>();
    }
    
    [Test]
    public void GetAllInstances_Generic_ShouldGetInstancesFromStructuremapContainer()
    {
      //---------------Set up test pack-------------------
      var container    = new Mock<IContainer>();
      var iocContainer = new StructuremapThuriaIocContainer(container.Object);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      iocContainer.GetAllInstances<IFakeObject>();
      //---------------Test Result -----------------------
      container.Verify(container1 => container1.GetAllInstances<IFakeObject>(), Times.Once);
    }
    
    [Test]
    public void GetAllInstances_Generic_GivenObjectsNotFound_ShouldNotThrowExceptionAndReturnEmptyList()
    {
      //---------------Set up test pack-------------------
      var container                            = new Container();
      var iocContainer                         = new StructuremapThuriaIocContainer(container);
      IEnumerable<IFakeObject> returnedObjects = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => returnedObjects = iocContainer.GetAllInstances<IFakeObject>());
      //---------------Test Result -----------------------
      var fakeObjects = returnedObjects as IFakeObject[] ?? returnedObjects.ToArray();
      fakeObjects.Should().NotBeNull();
      fakeObjects.Any().Should().BeFalse();
    }
    
    [Test]
    public void GetAllInstances_Generic_GivenObjectsExists_ShouldNotThrowExceptionAndReturnListOfObjects()
    {
      //---------------Set up test pack-------------------
      var fakeObject1 = new FakeObject();
      var fakeObject2 = new FakeObject();
      var container   = new Container();
      container.Inject(typeof(IFakeObject), fakeObject1);
      container.Inject(typeof(IFakeObject), fakeObject2);
    
      var iocContainer = new StructuremapThuriaIocContainer(container);
      IEnumerable<IFakeObject> returnedObjects = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => returnedObjects = iocContainer.GetAllInstances<IFakeObject>());
      //---------------Test Result -----------------------
      var fakeObjects = returnedObjects as IFakeObject[] ?? returnedObjects.ToArray();
      fakeObjects.Should().NotBeNull();
      fakeObjects.Any().Should().BeTrue();
      fakeObjects.Count().Should().Be(2);
    }
    
    [Test]
    public void GetAllInstances_ShouldGetInstancesFromStructuremapContainer()
    {
      //---------------Set up test pack-------------------
      var container    = new Mock<IContainer>();
      var iocContainer = new StructuremapThuriaIocContainer(container.Object);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      iocContainer.GetAllInstances(typeof(IFakeObject));
      //---------------Test Result -----------------------
      container.Verify(container1 => container1.GetAllInstances(typeof(IFakeObject)), Times.Once);
    }
    
    [Test]
    public void GetAllInstances_GivenObjectsNotFound_ShouldNotThrowExceptionAndReturnEmptyList()
    {
      //---------------Set up test pack-------------------
      var container                       = new Container();
      var iocContainer                    = new StructuremapThuriaIocContainer(container);
      IEnumerable<object> returnedObjects = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => returnedObjects = iocContainer.GetAllInstances(typeof(IFakeObject)));
      //---------------Test Result -----------------------
      var enumerable = returnedObjects as object[] ?? returnedObjects.ToArray();
      enumerable.Should().NotBeNull();
      enumerable.Any().Should().BeFalse();
    }
    
    [Test]
    public void GetAllInstances_GivenObjectsExists_ShouldNotThrowExceptionAndReturnListOfObjects()
    {
      //---------------Set up test pack-------------------
      var fakeObject1 = new FakeObject();
      var fakeObject2 = new FakeObject();
      var container   = new Container();
      container.Configure(expression =>
        {
          expression.For<IFakeObject>().Use(fakeObject1);
          expression.For<IFakeObject>().Use(fakeObject2);
        });
    
      var iocContainer                    = new StructuremapThuriaIocContainer(container);
      IEnumerable<object> returnedObjects = null;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => returnedObjects = iocContainer.GetAllInstances(typeof(IFakeObject)));
      //---------------Test Result -----------------------
      var enumerable = returnedObjects as object[] ?? returnedObjects.ToArray();
      enumerable.Should().NotBeNull();
      enumerable.Count().Should().Be(2);
    }

    public interface IFakeObject
    {
    }

    public class FakeObject : IFakeObject
    {
    }
  }
}
