using System.Collections.Generic;
using Thuria.Zitidar.Core;

namespace Thuria.Zitidar.Serialization
{
  /// <inheritdoc />
  public abstract class ThuriaBaseJsonSerializerSettings : IThuriaSerializerSettings
  {
    /// <inheritdoc />
    public dynamic ConstructorHandling { get; set; } = Newtonsoft.Json.ConstructorHandling.Default;
    
    /// <inheritdoc />
    public dynamic ContractResolver { get; set; } = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
    
    /// <inheritdoc />
    public dynamic TypeNameHandling { get; set; } = Newtonsoft.Json.TypeNameHandling.None;
    
    /// <inheritdoc />
    public dynamic TypeNameAssemblyFormatHandling { get; set; } = Newtonsoft.Json.TypeNameAssemblyFormatHandling.Full;
    
    /// <inheritdoc />
    public dynamic Formatting { get; set; } = Newtonsoft.Json.Formatting.None;
    
    /// <inheritdoc />
    public dynamic NullValueHandling { get; set; } = Newtonsoft.Json.NullValueHandling.Include;
    
    /// <inheritdoc />
    public dynamic ReferenceLoopHandling { get; set; } = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
    
    /// <inheritdoc />
    public dynamic PreserveReferencesHandling { get; set; } = Newtonsoft.Json.PreserveReferencesHandling.None;
    
    /// <inheritdoc />
    public dynamic DateTimeZoneHandling { get; set; } = Newtonsoft.Json.DateTimeZoneHandling.Local;
    
    /// <inheritdoc />
    public dynamic DateFormatHandling { get; set; } = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
    
    /// <inheritdoc />
    public dynamic SerializationBinder { get; set; }

    /// <inheritdoc />
    public IEnumerable<dynamic> Converters { get; set; }
  }
}
