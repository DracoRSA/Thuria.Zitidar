using System;
using System.Collections.Generic;

using Lamar;
using Lamar.IoC.Instances;
using Microsoft.Extensions.DependencyInjection;
using Thuria.Zitidar.Core;

namespace Thuria.Zitidar.Lamar
{
  public class LamarThuriaBootstrapper : ILamarThuriaBootstrapper
  {
    private readonly List<ServiceRegistry> _serviceRegistries = new List<ServiceRegistry>();
    private readonly List<Tuple<Type, Type>> _objectMappings = new List<Tuple<Type, Type>>();
    private readonly List<Tuple<Type, object>> _concreteObjectMappings = new List<Tuple<Type, object>>();

    protected bool _scanFiles = true;

    public IThuriaIocContainer IocContainer { get; private set; }

    public static LamarThuriaBootstrapper Create => new LamarThuriaBootstrapper();

    public IContainer Container { get; private set; }
    
    public ILamarThuriaBootstrapper WithScanningOfFiles(bool isEnabled)
    {
      _scanFiles = isEnabled;
      return this;
    }

    public ILamarThuriaBootstrapper WithRegistry(ServiceRegistry serviceRegistry)
    {
      _serviceRegistries.Add(serviceRegistry);
      return this;
    }

    public ILamarThuriaBootstrapper WithObjectMapping(Type pluginType, Type concreteType)
    {
      var objectMap = new Tuple<Type, Type>(pluginType, concreteType);
      _objectMappings.Add(objectMap);
    
      return this;
    }

    public ILamarThuriaBootstrapper WithConcreteObjectMapping(Type pluginType, object concreteObject)
    {
      _concreteObjectMappings.Add(new Tuple<Type, object>(pluginType, concreteObject));
      return this;
    }

    public IThuriaBootstrapper Build()
    {
      Container = new Container(expression =>
        {
          if (_scanFiles)
          {
            expression.Scan(scanner =>
              {
                scanner.AssembliesAndExecutablesFromApplicationBaseDirectory();
                scanner.LookForRegistries();
                scanner.WithDefaultConventions();
              });
          }

          expression.For<IThuriaIocContainer>().Use<LamarThuriaIocContainer>();

          foreach (var currentObjectMapping in _objectMappings)
          {
            expression.For(currentObjectMapping.Item1).Use(currentObjectMapping.Item2);
          }

          foreach (var currentObjectMapping in _concreteObjectMappings)
          {
            var serviceDescriptor = new ServiceDescriptor(currentObjectMapping.Item1, currentObjectMapping.Item2);
            expression.Add(serviceDescriptor);
          }

          foreach (var serviceRegistry in _serviceRegistries)
          {
            expression.AddRange(serviceRegistry);
          }
        });

      Container.AssertConfigurationIsValid(AssertMode.Full);

      IocContainer = Container.GetInstance<IThuriaIocContainer>();
      return this;
    }
  }
}
