using System;
using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json.Serialization;

namespace Thuria.Zitidar.Serialization
{
  /// <summary>
  /// Core To Fule Types Serialization Binder
  /// </summary>
  public class CoreToFullTypesBinder : ISerializationBinder
  {
    private readonly List<Type> _supportedTypes = new List<Type>
      {
        typeof(decimal[]),
        typeof(decimal)
      };

    /// <summary>
    /// Bind To Type
    /// </summary>
    /// <param name="assemblyName">Assembly Name</param>
    /// <param name="typeName">Type Name</param>
    /// <returns>Type to bind to</returns>
    public Type BindToType(string assemblyName, string typeName)
    {
      return _supportedTypes.Any(type => type.FullName == typeName)
               ? _supportedTypes.FirstOrDefault(type => type.FullName == typeName)
               : Type.GetType($"{typeName}, {assemblyName}");
    }

    /// <summary>
    /// Bind To Name
    /// </summary>
    /// <param name="serializedType">Serialized Type</param>
    /// <param name="assemblyName">Assembly Name</param>
    /// <param name="typeName">Type Name</param>
    public void BindToName(Type serializedType, out string assemblyName, out string typeName)
    {
      if (_supportedTypes.Any(type => type.Name == serializedType.Name))
      {
        assemblyName = null;
        typeName     = serializedType.FullName;
      }
      else
      {
        assemblyName = serializedType.Assembly.FullName;
        typeName     = serializedType.FullName;
      }
    }
  }
}
