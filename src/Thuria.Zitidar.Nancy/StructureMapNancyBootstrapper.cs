using System;
using System.Linq;
using System.Collections.Generic;

using Nancy;
using StructureMap;
using Nancy.ViewEngines;
using Nancy.Diagnostics;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using StructureMap.Pipeline;

namespace Thuria.Zitidar.Nancy
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
  public abstract class StructureMapNancyBootstrapper : NancyBootstrapperWithRequestContainerBase<IContainer>, IDisposable
  {
    private bool _isDisposing;
    private readonly IContainer _container;

    protected StructureMapNancyBootstrapper(IContainer structuremapContainer)
    {
      _container = structuremapContainer ?? throw new ArgumentNullException(nameof(structuremapContainer));
    }

    protected override INancyEnvironmentConfigurator GetEnvironmentConfigurator()
    {
      return ApplicationContainer.GetInstance<INancyEnvironmentConfigurator>();
    }

    protected override IDiagnostics GetDiagnostics()
    {
      return ApplicationContainer.GetInstance<IDiagnostics>();
    }

    protected override IEnumerable<IApplicationStartup> GetApplicationStartupTasks()
    {
      return ApplicationContainer.GetAllInstances<IApplicationStartup>();
    }

    protected override IEnumerable<IRequestStartup> RegisterAndGetRequestStartupTasks(IContainer container, Type[] requestStartupTypes)
    {
      return requestStartupTypes.Select(container.GetInstance).Cast<IRequestStartup>().ToArray();
    }

    protected override IEnumerable<IRegistrations> GetRegistrationTasks()
    {
      return ApplicationContainer.GetAllInstances<IRegistrations>();
    }

    public override INancyEnvironment GetEnvironment()
    {
      return ApplicationContainer.GetInstance<INancyEnvironment>();
    }

    protected override INancyEngine GetEngineInternal()
    {
      return ApplicationContainer.GetInstance<INancyEngine>();
    }

    protected override IContainer GetApplicationContainer()
    {
      return _container;
    }

    protected override void RegisterNancyEnvironment(IContainer container, INancyEnvironment environment)
    {
      _container.Configure(registry => registry.For<INancyEnvironment>().Use(environment));
    }

    protected override void RegisterBootstrapperTypes(IContainer applicationContainer)
    {
      applicationContainer.Configure(registry =>
      {
        registry.For<INancyModuleCatalog>().Singleton().Use(this);
        registry.For<IFileSystemReader>().Singleton().Use<DefaultFileSystemReader>();
      });
    }

    protected override void RegisterTypes(IContainer container, IEnumerable<TypeRegistration> typeRegistrations)
    {
      container.Configure(registry =>
      {
        foreach (var typeRegistration in typeRegistrations)
        {
          RegisterType(typeRegistration.RegistrationType,
                       typeRegistration.ImplementationType,
                       container.Role == ContainerRole.Nested ? Lifetime.PerRequest : typeRegistration.Lifetime,
                       registry);
        }
      });
    }

    protected override void RegisterCollectionTypes(IContainer container, IEnumerable<CollectionTypeRegistration> collectionTypeRegistrationsn)
    {
      container.Configure(registry =>
      {
        foreach (var collectionTypeRegistration in collectionTypeRegistrationsn)
        {
          foreach (var implementationType in collectionTypeRegistration.ImplementationTypes)
          {
            RegisterType(collectionTypeRegistration.RegistrationType,
                         implementationType,
                         container.Role == ContainerRole.Nested ? Lifetime.PerRequest : collectionTypeRegistration.Lifetime,
                         registry);
          }
        }
      });
    }

    protected override void RegisterInstances(IContainer container, IEnumerable<InstanceRegistration> instanceRegistrations)
    {
      container.Configure(registry =>
      {
        foreach (var instanceRegistration in instanceRegistrations)
        {
          registry.For(instanceRegistration.RegistrationType).LifecycleIs(Lifecycles.Singleton).Use(instanceRegistration.Implementation);
        }
      });
    }

    protected override IContainer CreateRequestContainer(NancyContext context)
    {
      return ApplicationContainer.GetNestedContainer();
    }

    protected override void RegisterRequestContainerModules(IContainer container, IEnumerable<ModuleRegistration> moduleRegistrationTypes)
    {
      container.Configure(registry =>
      {
        foreach (var registrationType in moduleRegistrationTypes)
        {
          registry.For(typeof(INancyModule)).LifecycleIs(Lifecycles.Unique).Use(registrationType.ModuleType);
        }
      });
    }

    protected override IEnumerable<INancyModule> GetAllModules(IContainer container)
    {
      return container.GetAllInstances<INancyModule>();
    }

    protected override INancyModule GetModule(IContainer container, Type moduleType)
    {
      return (INancyModule)container.GetInstance(moduleType);
    }

    public new void Dispose()
    {
      if (this._isDisposing)
      {
        return;
      }

      this._isDisposing = true;
      base.Dispose();
    }

    private void RegisterType(Type registrationType, Type implementationType, Lifetime lifetime, IProfileRegistry registry)
    {
      switch (lifetime)
      {
        case Lifetime.Transient:
          registry.For(registrationType).LifecycleIs(Lifecycles.Unique).Use(implementationType);
          break;
        case Lifetime.Singleton:
          registry.For(registrationType).LifecycleIs(Lifecycles.Singleton).Use(implementationType);
          break;
        case Lifetime.PerRequest:
          registry.For(registrationType).Use(implementationType);
          break;
        default:
          throw new ArgumentOutOfRangeException("lifetime", lifetime, String.Format("Unknown Lifetime: {0}.", lifetime));
      }
    }
  }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}