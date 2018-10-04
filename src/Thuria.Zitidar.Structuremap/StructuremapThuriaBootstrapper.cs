using System;
using System.Collections.Generic;

using StructureMap;

using Thuria.Zitidar.Core;

namespace Thuria.Zitidar.Structuremap
{
  public class StructuremapThuriaBootstrapper : IStructuremapThuriaBootstrapper
  {
    protected bool _scanFiles = true;

    protected readonly List<Registry> _iocRegistries                     = new List<Registry>();
    protected readonly List<Tuple<Type, Type>> _objectMappings           = new List<Tuple<Type, Type>>();
    protected readonly List<Tuple<Type, object>> _objectConcreteMappings = new List<Tuple<Type, object>>();

    public IThuriaIocContainer IocContainer { get; private set; }

    public static StructuremapThuriaBootstrapper Create()
    {
      return new StructuremapThuriaBootstrapper();
    }

    public IContainer Container { get; protected set; }

    public IStructuremapThuriaBootstrapper WithScannningOfFiles(bool isEnabled)
    {
      _scanFiles = isEnabled;
      return this;
    }

    public IStructuremapThuriaBootstrapper WithRegistry(Registry iocRegistry)
    {
      _iocRegistries.Add(iocRegistry);
      return this;
    }

    public IStructuremapThuriaBootstrapper WithObjectMapping(Type pluginType, Type concreteType)
    {
      var objectMap = new Tuple<Type, Type>(pluginType, concreteType);
      _objectMappings.Add(objectMap);

      return this;
    }

    public IStructuremapThuriaBootstrapper WithConcreteObjectMapping(Type pluginType, object concreteObject)
    {
      var objectMap = new Tuple<Type, object>(pluginType, concreteObject);
      _objectConcreteMappings.Add(objectMap);

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

          foreach (var currentIocRegistry in _iocRegistries)
          {
            expression.AddRegistry(currentIocRegistry);
          }

          expression.For<IThuriaIocContainer>().Use<StructuremapThuriaIocContainer>();

          foreach (var currentObjectMapping in _objectMappings)
          {
            expression.For(currentObjectMapping.Item1).Use(currentObjectMapping.Item2);
          }

          foreach (var currentObjectMapping in _objectConcreteMappings)
          {
            expression.For(currentObjectMapping.Item1).Use(currentObjectMapping.Item2);
          }
        });

      IocContainer = Container.GetInstance<IThuriaIocContainer>();
      return this;
    }
  }
}
