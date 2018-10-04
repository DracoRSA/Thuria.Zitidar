using System;
using StructureMap;
using Thuria.Zitidar.Core;

namespace Thuria.Zitidar.Structuremap
{
  public interface IStructuremapThuriaBootstrapper : IThuriaBootstrapper
  {
    IContainer Container { get; }

    IStructuremapThuriaBootstrapper WithScannningOfFiles(bool isEnabled);
    IStructuremapThuriaBootstrapper WithRegistry(Registry iocRegistry);
    IStructuremapThuriaBootstrapper WithObjectMapping(Type pluginType, Type concreteType);
    IStructuremapThuriaBootstrapper WithConcreteObjectMapping(Type pluginType, object concreteObject);
  }
}
