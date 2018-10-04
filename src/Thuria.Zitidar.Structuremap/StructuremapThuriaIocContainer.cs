using System;
using System.Linq;
using System.Collections.Generic;

using StructureMap;

using Thuria.Zitidar.Core;

namespace Thuria.Zitidar.Structuremap
{
  /// <inheritdoc />
  public class StructuremapThuriaIocContainer : IThuriaIocContainer
  {
    public StructuremapThuriaIocContainer(IContainer iocContainer)
    {
      Container = iocContainer ?? throw new ArgumentNullException(nameof(iocContainer));
    }

    /// <summary>
    /// Structuremap Container
    /// </summary>
    public IContainer Container { get; }

    /// <inheritdoc />
    public T GetInstance<T>()
    {
      return Container.TryGetInstance<T>();
    }

    /// <inheritdoc />
    public T GetInstance<T>(string instanceName)
    {
      return Container.TryGetInstance<T>(instanceName);
    }

    /// <inheritdoc />
    public object GetInstance(Type requiredType)
    {
      return Container.TryGetInstance(requiredType);
    }

    /// <inheritdoc />
    public object GetInstance(Type requiredType, string instanceName)
    {
      return string.IsNullOrWhiteSpace(instanceName)
                        ? Container.TryGetInstance(requiredType)
                        : Container.TryGetInstance(requiredType, instanceName);
    }

    /// <inheritdoc />
    public IEnumerable<T> GetAllInstances<T>()
    {
      return Container.GetAllInstances<T>();
    }

    /// <inheritdoc />
    public IEnumerable<object> GetAllInstances(Type instanceType)
    {
      return Container.GetAllInstances(instanceType).Cast<object>();
    }
  }
}
