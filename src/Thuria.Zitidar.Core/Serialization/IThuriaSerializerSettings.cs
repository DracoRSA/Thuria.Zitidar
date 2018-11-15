using System.Collections.Generic;

namespace Thuria.Zitidar.Core
{
  /// <summary>
  /// Thuria Serializer Settings (Based on the Newtonsoft.JSON serialization framework />
  /// </summary>
  public interface IThuriaSerializerSettings
  {
    /// <summary>
    /// Constructor Handling
    /// </summary>
    dynamic ConstructorHandling { get; set; }

    /// <summary>
    /// Contract Resolver
    /// </summary>
    dynamic ContractResolver { get; set; }

    /// <summary>
    /// Type Name Handling
    /// </summary>
    dynamic TypeNameHandling { get; set; }

    /// <summary>
    /// Type Name Assembly Format Handling
    /// </summary>
    dynamic TypeNameAssemblyFormatHandling { get; set; }

    /// <summary>
    /// Formatting
    /// </summary>
    dynamic Formatting { get; set; }

    /// <summary>
    /// Null Value handling
    /// </summary>
    dynamic NullValueHandling { get; set; }

    /// <summary>
    /// Reference Loop Handling
    /// </summary>
    dynamic ReferenceLoopHandling { get; set; }

    /// <summary>
    /// Preserve References Handling
    /// </summary>
    dynamic PreserveReferencesHandling { get; set; }

    /// <summary>
    /// Date Time Zone Handling
    /// </summary>
    dynamic DateTimeZoneHandling { get; set; }

    /// <summary>
    /// Date Format Handling
    /// </summary>
    dynamic DateFormatHandling { get; set; }

    /// <summary>
    /// Serialization Binder
    /// </summary>
    dynamic SerializationBinder { get; set; }

    /// <summary>
    /// Converters
    /// </summary>
    IEnumerable<dynamic> Converters { get; set; }
  }
}
