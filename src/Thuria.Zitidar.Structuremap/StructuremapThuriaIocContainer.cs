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
    private readonly IContainer _rawContainer;

    /// <summary>
    /// Ioc Container Constructor
    /// </summary>
    /// <param name="iocContainer"></param>
    public StructuremapThuriaIocContainer(IContainer iocContainer)
    {
      _rawContainer = iocContainer ?? throw new ArgumentNullException(nameof(iocContainer));
    }

    /// <summary>
    /// Structuremap Container
    /// </summary>
    public object Container => _rawContainer;

    /// <inheritdoc />
    public T GetInstance<T>()
    {
      return _rawContainer.TryGetInstance<T>();
    }

    /// <inheritdoc />
    public T GetInstance<T>(string instanceName)
    {
      return _rawContainer.TryGetInstance<T>(instanceName);
    }

    /// <inheritdoc />
    public object GetInstance(Type requiredType)
    {
      return _rawContainer.TryGetInstance(requiredType);
    }

    /// <inheritdoc />
    public object GetInstance(Type requiredType, string instanceName)
    {
      return string.IsNullOrWhiteSpace(instanceName)
                        ? _rawContainer.TryGetInstance(requiredType)
                        : _rawContainer.TryGetInstance(requiredType, instanceName);
    }

    /// <inheritdoc />
    public IEnumerable<T> GetAllInstances<T>()
    {
      return _rawContainer.GetAllInstances<T>();
    }

    /// <inheritdoc />
    public IEnumerable<object> GetAllInstances(Type instanceType)
    {
      return _rawContainer.GetAllInstances(instanceType).Cast<object>();
    }
  }
}
