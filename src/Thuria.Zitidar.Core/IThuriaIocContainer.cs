using System;
using System.Collections.Generic;

namespace Thuria.Zitidar.Core
{
  /// <summary>
  /// Thuria IOC Container
  /// </summary>
  public interface IThuriaIocContainer
  {
    /// <summary>
    /// Underlying Raw Container
    /// </summary>
    object Container { get; }

    /// <summary>
    /// Get an instance of type T from the container
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <returns>Object of type T as requested</returns>
    T GetInstance<T>();

    /// <summary>
    /// Get a named instance of type T from the container
    /// </summary>
    /// <param name="instanceName">Name of the instance</param>
    /// <returns>Named instance of Object of type T as requested</returns>
    T GetInstance<T>(string instanceName);

    /// <summary>
    /// Get an instance of Type from the container
    /// </summary>
    /// <param name="requiredType">Instance Type</param>
    /// <returns>An object of the specified Type</returns>
    object GetInstance(Type requiredType);

    /// <summary>
    /// Get a named instance of Type from the container
    /// </summary>
    /// <param name="requiredType">Instance Type</param>
    /// <param name="instanceName">Name of the instance</param>
    /// <returns>An object of the specified Type registered with the specified name</returns>
    object GetInstance(Type requiredType, string instanceName);

    /// <summary>
    /// Get a list of items of Type T from the container
    /// </summary>
    /// <typeparam name="T">Instance Type</typeparam>
    /// <returns>A list of instances implementing Type T</returns>
    IEnumerable<T> GetAllInstances<T>();

    /// <summary>
    /// Get a list of items of Type from the container
    /// </summary>
    /// <param name="instanceType">Instance Type</param>
    /// <returns>A list of instances implementing Type</returns>
    IEnumerable<object> GetAllInstances(Type instanceType);
  }
}
