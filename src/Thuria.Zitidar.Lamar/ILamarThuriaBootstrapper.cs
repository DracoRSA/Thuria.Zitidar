using System;
using Lamar;
using Thuria.Zitidar.Core;

namespace Thuria.Zitidar.Lamar
{
  public interface ILamarThuriaBootstrapper : IThuriaBootstrapper
  {
    IContainer Container { get; }
    
    ILamarThuriaBootstrapper WithScanningOfFiles(bool isEnabled);
    ILamarThuriaBootstrapper WithRegistry(ServiceRegistry serviceRegistry);
    ILamarThuriaBootstrapper WithObjectMapping(Type pluginType, Type concreteType);
    ILamarThuriaBootstrapper WithConcreteObjectMapping(Type pluginType, object concreteObject);
  }
}
