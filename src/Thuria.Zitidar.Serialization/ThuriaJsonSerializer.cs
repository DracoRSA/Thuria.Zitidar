using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using Thuria.Zitidar.Core;

namespace Thuria.Zitidar.Serialization
{
  /// <summary>
  /// Thuria JSON Serializer
  /// </summary>
  public class ThuriaJsonSerializer : IThuriaSerializer
  {
    private readonly IThuriaSerializerSettings _jsonSerializerSettings;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="serializerSettings"></param>
    public ThuriaJsonSerializer(IThuriaSerializerSettings serializerSettings)
    {
      _jsonSerializerSettings = serializerSettings ?? throw new ArgumentNullException(nameof(serializerSettings));
    }

    /// <inheritdoc />
    public string SerializeObject<T>(T objectToSerialize, IThuriaSerializerSettings serializerSettings = null)
    {
      if (objectToSerialize == null) { return null; }

      var settings   = GetJsonSerializerSettings(serializerSettings);
      var jsonString = new StringBuilder();

      using (var writer = new JsonTextWriter(new StringWriter(jsonString)))
      {
        var jsonSerializer = JsonSerializer.Create(settings);
        jsonSerializer.Serialize(writer, objectToSerialize);
      }

      var serializedJsonData = jsonString.ToString();
      if (string.IsNullOrWhiteSpace(serializedJsonData) || serializedJsonData == "null")
      {
        throw new Exception("Failed to serialize object");
      }

      return serializedJsonData.Replace("$type", "_name");
    }

    /// <inheritdoc />
    public T DeserializeObject<T>(string jsonString, bool throwException = false)
    {
      try
      {
        var serializerSettings         = this.GetJsonSerializerSettings();
        var jsonStringWithTypeVariable = jsonString.Replace("_name", "$type");
        var jObject                    = JObject.Parse(jsonStringWithTypeVariable);

        var jTypeToken = jObject["$type"];
        var objectType = (jTypeToken == null || Type.GetType(jTypeToken.ToString()) == null)
                                    ? typeof(T)
                                    : Type.GetType(jTypeToken.ToString());

        return (T)JsonConvert.DeserializeObject(jsonStringWithTypeVariable, objectType, serializerSettings);
      }
      catch (Exception runtimeException)
      {
        if (throwException) { throw; }

        Debug.WriteLine($"Failed to Deserialize Object\n{runtimeException}");
        return default(T);
      }
    }

    /// <inheritdoc />
    public object DeserializeObject(string jsonString, bool throwException = false)
    {
      try
      {
        var serializerSettings         = this.GetJsonSerializerSettings();
        var jsonStringWithTypeVariable = jsonString.Replace("_name", "$type");
        var jObject                    = JObject.Parse(jsonStringWithTypeVariable);

        var jTypeToken = jObject["$type"];
        var objectType = (jTypeToken == null || Type.GetType(jTypeToken.ToString()) == null)
                                    ? null
                                    : Type.GetType(jTypeToken.ToString());

        return JsonConvert.DeserializeObject(jsonStringWithTypeVariable, objectType, serializerSettings);
      }
      catch (Exception runtimeException)
      {
        if (throwException) { throw; }

        Debug.WriteLine($"Failed to Deserialize Object\n{runtimeException}");
        return null;
      }
    }

    /// <summary>
    /// Convert the Thuria Serializer Settings to the Newtonsoft Json Serializer settings
    /// </summary>
    /// <param name="serializerSettings">Thuria Serializer Settings (Optional)</param>
    /// <param name="traceWriter">Trace Writer to use when doing serialization</param>
    /// <returns>A Newtonsoft JSON Serializer settings object</returns>
    protected JsonSerializerSettings GetJsonSerializerSettings(IThuriaSerializerSettings serializerSettings = null, ITraceWriter traceWriter = null)
    {
      var jsonSerializerSettings = serializerSettings ?? _jsonSerializerSettings;

      var jsonSettings = new JsonSerializerSettings
        {
          ConstructorHandling            = jsonSerializerSettings.ConstructorHandling,
          ContractResolver               = jsonSerializerSettings.ContractResolver,
          TypeNameHandling               = jsonSerializerSettings.TypeNameHandling,
          TypeNameAssemblyFormatHandling = jsonSerializerSettings.TypeNameAssemblyFormatHandling,
          Formatting                     = jsonSerializerSettings.Formatting,
          NullValueHandling              = jsonSerializerSettings.NullValueHandling,
          ReferenceLoopHandling          = jsonSerializerSettings.ReferenceLoopHandling,
          PreserveReferencesHandling     = jsonSerializerSettings.PreserveReferencesHandling,
          DateTimeZoneHandling           = jsonSerializerSettings.DateTimeZoneHandling,
          DateFormatHandling             = jsonSerializerSettings.DateFormatHandling,
        };

      if (jsonSerializerSettings.SerializationBinder != null)
      {
        jsonSettings.SerializationBinder = jsonSerializerSettings.SerializationBinder;
      }

      if (jsonSerializerSettings.Converters != null && jsonSerializerSettings.Converters.Any())
      {
        jsonSettings.Converters = jsonSerializerSettings.Converters.Cast<JsonConverter>().ToList();
      }

      if (traceWriter != null)
      {
        jsonSettings.TraceWriter = traceWriter;
      }

      return jsonSettings;
    }
  }
}
