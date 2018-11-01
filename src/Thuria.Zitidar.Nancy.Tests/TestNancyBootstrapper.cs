using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NSubstitute;
using NUnit.Framework;
using FluentAssertions;

using Nancy;
using StructureMap;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Nancy.ViewEngines;
using Thuria.Calot.TestUtilities;
using Thuria.Zitidar.Structuremap;

namespace Thuria.Zitidar.Nancy.Tests
{
  [TestFixture]
  public class TestNancyBootstrapper
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var container = Substitute.For<IContainer>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var nancyBootstrapper = new NancyBootstrapper(container);
      //---------------Test Result -----------------------
      nancyBootstrapper.Should().NotBeNull();
    }

    [TestCase("structuremapContainer")]
    public void Constructor_GivenNullParameter_ShouldThrowException(string parameterName)
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      ConstructorTestHelper.ValidateArgumentNullExceptionIfParameterIsNull<NancyBootstrapper>(parameterName);
      //---------------Test Result -----------------------
    }

    [Test]
    public void GetApplicationContainer_ShouldReturnContainerGivenInConstructor()
    {
      //---------------Set up test pack-------------------
      var container         = Substitute.For<IContainer>();
      var nancyBootstrapper = new FakeNancyBootstrapper(container);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var applicationContainer = nancyBootstrapper.GetApplicationContainer();
      //---------------Test Result -----------------------
      applicationContainer.Should().NotBeNull();
      applicationContainer.Should().Be(container);
    }

    [Test]
    public void ApplicationStartup_ShouldNotThrowException()
    {
      //---------------Set up test pack-------------------
      var iocContainer      = CreateIocContainer();
      var pipelines         = new Pipelines();
      var container         = (IContainer) iocContainer.Container;
      var nancyBootstrapper = new FakeNancyBootstrapper(container);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => nancyBootstrapper.ApplicationStartup(container, pipelines));
      //---------------Test Result -----------------------
    }

    [Test]
    public void RequestStartup_ShouldNotThrowException()
    {
      //---------------Set up test pack-------------------
      var iocContainer      = CreateIocContainer();
      var container         = (IContainer) iocContainer.Container;
      var pipelines         = new Pipelines();
      var nancyBootstrapper = new FakeNancyBootstrapper(container);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => nancyBootstrapper.RequestStartup(container, pipelines, new NancyContext()));
      //---------------Test Result -----------------------
    }
    
    [Test]
    public void RequestStartup_ShouldConfigureOnError()
    {
      //---------------Set up test pack-------------------
      var iocContainer      = CreateIocContainer();
      var container         = (IContainer) iocContainer.Container;
      var pipelines         = new Pipelines();
      var nancyBootstrapper = new FakeNancyBootstrapper(container);
      //---------------Assert Precondition----------------
      Assert.That(pipelines.OnError.PipelineItems.Count(), Is.EqualTo(0));
      //---------------Execute Test ----------------------
      nancyBootstrapper.RequestStartup(container, pipelines, new NancyContext());
      //---------------Test Result -----------------------
      Assert.That(pipelines.OnError.PipelineItems.Count(), Is.EqualTo(1));
    }
    
    [Test]
    public void RequestStartup_ShouldConfigureOnError_ShouldAddExceptionDetailToHeaderWhenCalled()
    {
      //---------------Set up test pack-------------------
      var exceptionMessage  = "Test Exception";
      var testException     = new Exception(exceptionMessage);
      var iocContainer      = CreateIocContainer();
      var container         = (IContainer) iocContainer.Container;
      var pipelines         = new Pipelines();
      var nancyBootstrapper = new FakeNancyBootstrapper(container);
      var nancyContext      = new NancyContext();
    
      nancyBootstrapper.RequestStartup(container, pipelines, nancyContext);
      //---------------Assert Precondition----------------
      Assert.That(pipelines.OnError.PipelineItems.Count(), Is.EqualTo(1));
      //---------------Execute Test ----------------------
      pipelines.OnError.PipelineDelegates.First().Invoke(nancyContext, testException);
      //---------------Test Result -----------------------
      Assert.That(nancyContext.Items.Count, Is.EqualTo(1));
      Assert.That(nancyContext.Items.Keys.Contains("OnErrorContextHook"));
      Assert.That(nancyContext.Items["OnErrorContextHook"], Is.EqualTo(testException));
    }
    
    [Test]
    public void Configure_ShouldNotThrowException()
    {
      //---------------Set up test pack-------------------
      var iocContainer      = CreateIocContainer();
      var nancyBootstrapper = new FakeNancyBootstrapper((IContainer) iocContainer.Container);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => nancyBootstrapper.Configure(new DefaultNancyEnvironment()));
      //---------------Test Result -----------------------
    }
    
    [Test]
    public void InternalConfiguration_ShouldNotThrowException()
    {
      //---------------Set up test pack-------------------
      var iocContainer      = CreateIocContainer();
      var nancyBootstrapper = new FakeNancyBootstrapper((IContainer) iocContainer.Container);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => nancyBootstrapper.ExecuteInternalConfiguration());
      //---------------Test Result -----------------------
    }
    
    [Test]
    public void GetApplicationContainer_ShouldReturnContainer()
    {
      //---------------Set up test pack-------------------
      var iocContainer      = CreateIocContainer();
      var nancyBootstrapper = new FakeNancyBootstrapper((IContainer) iocContainer.Container);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var applicationContainer = nancyBootstrapper.GetApplicationContainer();
      //---------------Test Result -----------------------
      Assert.That(applicationContainer, Is.EqualTo(iocContainer.Container));
    }
    
    [Test]
    public void RegisterBoostrapperTypes_ShouldReturnContainerWithConfiguredTypes()
    {
      //---------------Set up test pack-------------------
      var iocContainer      = CreateIocContainer();
      var container         = (IContainer) iocContainer.Container;
      var nancyBootstrapper = new FakeNancyBootstrapper(container);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      nancyBootstrapper.RegisterBootstrapperTypes(container);
      //---------------Test Result -----------------------
      Assert.That(iocContainer.GetInstance<INancyModuleCatalog>(), Is.Not.Null);
      Assert.That(iocContainer.GetInstance<IFileSystemReader>(), Is.Not.Null);
    }
    
    [Test]
    public void RegisterTypes_ShouldReturnContainerWithSuppliedTypesRegistered()
    {
      //---------------Set up test pack-------------------
      var iocContainer      = CreateIocContainer();
      var container         = (IContainer) iocContainer.Container;
      var nancyBootstrapper = new FakeNancyBootstrapper(container);
      var typeRegistrations = new List<TypeRegistration>
                {
                    new TypeRegistration(typeof(IFakePerRequestNancyClass), typeof(FakePerRequestNancyClass), Lifetime.PerRequest),
                    new TypeRegistration(typeof(IFakeTransientNancyClass), typeof(FakeTransientNancyClass), Lifetime.Transient),
                    new TypeRegistration(typeof(IFakeSingletonNancyClass), typeof(FakeSingletonNancyClass), Lifetime.Singleton)
                };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      nancyBootstrapper.RegisterTypes(container, typeRegistrations);
      //---------------Test Result -----------------------
      Assert.That(iocContainer.GetInstance<IFakePerRequestNancyClass>(), Is.InstanceOf<FakePerRequestNancyClass>());
      Assert.That(iocContainer.GetInstance<IFakeTransientNancyClass>(), Is.InstanceOf<FakeTransientNancyClass>());
      Assert.That(iocContainer.GetInstance<IFakeSingletonNancyClass>(), Is.InstanceOf<FakeSingletonNancyClass>());
    }
    
    [Test]
    public void RegisterCollectionTypes_ShouldReturnContainerWithSuppliedTypesRegistered()
    {
      //---------------Set up test pack-------------------
      var iocContainer                = CreateIocContainer();
      var container                   = (IContainer) iocContainer.Container;
      var nancyBootstrapper           = new FakeNancyBootstrapper(container);
      var collectionTypeRegistrations = new List<CollectionTypeRegistration>
                {
                    new CollectionTypeRegistration(typeof(IFakePerRequestNancyClass), new List<Type> { typeof(FakePerRequestNancyClass), typeof(FakePerRequestNancyClass2) }, Lifetime.PerRequest),
                    new CollectionTypeRegistration(typeof(IFakeTransientNancyClass), new List<Type> { typeof(FakeTransientNancyClass), typeof(FakeTransientNancyClass2) }, Lifetime.Transient),
                    new CollectionTypeRegistration(typeof(IFakeSingletonNancyClass), new List<Type> { typeof(FakeSingletonNancyClass), typeof(FakeSingletonNancyClass2) }, Lifetime.Singleton),
                };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      nancyBootstrapper.RegisterCollectionTypes(container, collectionTypeRegistrations);
      //---------------Test Result -----------------------
      CollectionAssert.AllItemsAreInstancesOfType(iocContainer.GetAllInstances<IFakePerRequestNancyClass>(), typeof(IFakePerRequestNancyClass));
      CollectionAssert.AllItemsAreInstancesOfType(iocContainer.GetAllInstances<IFakeTransientNancyClass>(), typeof(IFakeTransientNancyClass));
      CollectionAssert.AllItemsAreInstancesOfType(iocContainer.GetAllInstances<IFakeSingletonNancyClass>(), typeof(IFakeSingletonNancyClass));
    }
    
    private StructuremapThuriaIocContainer CreateIocContainer()
    {
      var iocContainer = StructuremapThuriaBootstrapper
                                  .Create()
                                  .Build()
                                  .IocContainer as StructuremapThuriaIocContainer;
      return iocContainer;
    }

    private class FakeNancyBootstrapper : NancyBootstrapper
    {
      public FakeNancyBootstrapper(IContainer container) : base(container)
      {
      }

      public new void ApplicationStartup(IContainer container, IPipelines pipelines)
      {
        base.ApplicationStartup(container, pipelines);
      }

      public Func<ITypeCatalog, NancyInternalConfiguration> ExecuteInternalConfiguration()
      {
        return base.InternalConfiguration;
      }

      public new void RequestStartup(IContainer container, IPipelines pipelines, NancyContext context)
      {
        base.RequestStartup(container, pipelines, context);
      }

      public new IContainer GetApplicationContainer()
      {
        return base.GetApplicationContainer();
      }

      public new void RegisterBootstrapperTypes(IContainer applicationContainer)
      {
        base.RegisterBootstrapperTypes(applicationContainer);
      }

      public new void RegisterTypes(IContainer container, IEnumerable<TypeRegistration> typeRegistrations)
      {
        base.RegisterTypes(container, typeRegistrations);
      }

      public new void RegisterCollectionTypes(IContainer container, IEnumerable<CollectionTypeRegistration> collectionTypeRegistrations)
      {
        base.RegisterCollectionTypes(container, collectionTypeRegistrations);
      }
    }
  }
}
