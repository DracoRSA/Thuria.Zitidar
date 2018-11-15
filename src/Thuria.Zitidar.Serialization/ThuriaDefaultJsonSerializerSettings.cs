using System.Collections.Generic;

namespace Thuria.Zitidar.Serialization
{
  /// <summary>
  /// Thuria Default Json Serializer Settings
  /// </summary>
  public class ThuriaDefaultJsonSerializerSettings : ThuriaBaseJsonSerializerSettings
  {
    /// <summary>
    /// Constructor
    /// </summary>
    public ThuriaDefaultJsonSerializerSettings()
    {
      ConstructorHandling            = Newtonsoft.Json.ConstructorHandling.Default;
      ContractResolver               = new Newtonsoft.Json.Serialization.DefaultContractResolver();
      TypeNameHandling               = Newtonsoft.Json.TypeNameHandling.All;
      TypeNameAssemblyFormatHandling = Newtonsoft.Json.TypeNameAssemblyFormatHandling.Simple;
      Formatting                     = Newtonsoft.Json.Formatting.None;
      NullValueHandling              = Newtonsoft.Json.NullValueHandling.Include;
      ReferenceLoopHandling          = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
      PreserveReferencesHandling     = Newtonsoft.Json.PreserveReferencesHandling.All;
      DateTimeZoneHandling           = Newtonsoft.Json.DateTimeZoneHandling.Local;
      DateFormatHandling             = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
      SerializationBinder            = new CoreToFullTypesBinder();
      Converters                     = new List<dynamic> { new ArrayReferencePreservingConverter() };
    }
  }
}
