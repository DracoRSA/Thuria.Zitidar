using System;
using System.IO;
using System.Collections.Generic;

using Nancy.IO;
using Newtonsoft.Json;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;

using Thuria.Zitidar.Core;

namespace Thuria.Zitidar.Serialization.Nancy
{
  /// <summary>
  /// Thuria Nancy Json Serializer
  /// </summary>
  public class ThuriaNancyJsonSerializer : ThuriaJsonSerializer, IThuriaNancySerializer
  {
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="serializerSettings">Serializer Settings</param>
    public ThuriaNancyJsonSerializer(IThuriaSerializerSettings serializerSettings)
      : base(serializerSettings)
    {
    }

    /// <summary>
    /// Serializer Extensions
    /// </summary>
    public IEnumerable<string> Extensions { get; }

    /// <summary>
    /// Determine if the Media Can Serialize
    /// </summary>
    /// <param name="mediaRange">Media Range</param>
    /// <returns>A boolean indicating if serialization can be done or not</returns>
    public bool CanSerialize(MediaRange mediaRange)
    {
      return IsJsonType($"{mediaRange.Type}/{mediaRange.Subtype}");
    }

    /// <summary>
    /// Determine if the Media Can Deserialize
    /// </summary>
    /// <param name="mediaRange">Media Range</param>
    /// <param name="context">Binding Context</param>
    /// <returns>A boolean indicating if deserialization can be done or not</returns>
    public bool CanDeserialize(MediaRange mediaRange, BindingContext context)
    {
      return IsJsonType($"{mediaRange.Type}/{mediaRange.Subtype}");
    }

    /// <summary>
    /// Serialize the given model
    /// </summary>
    /// <typeparam name="TModel">Model Type</typeparam>
    /// <param name="mediaRange">Media</param>
    /// <param name="model">Model to serialize</param>
    /// <param name="outputStream">Output stream the serialization will be written to</param>
    public void Serialize<TModel>(MediaRange mediaRange, TModel model, Stream outputStream)
    {
      using (var jsonTextWriter = new JsonTextWriter(new StreamWriter(new UnclosableStreamWrapper(outputStream))))
      {
        var serializedData = SerializeObject(model);
        if (string.IsNullOrWhiteSpace(serializedData))
        {
          return;
        }

        jsonTextWriter.WriteRaw(serializedData);
        jsonTextWriter.Flush();
      }
    }

    /// <summary>
    /// Deserialize the Stream 
    /// </summary>
    /// <param name="mediaRange">Media</param>
    /// <param name="bodyStream">Stream of data to deserialize</param>
    /// <param name="context">Binding Context</param>
    /// <returns>Deserialized object</returns>
    public object Deserialize(MediaRange mediaRange, Stream bodyStream, BindingContext context)
    {
      using (var streamReader = new StreamReader(bodyStream))
      {
        return DeserializeObject(streamReader.ReadToEnd());
      }
    }

    private bool IsJsonType(string contentType)
    {
      if (string.IsNullOrEmpty(contentType))
      {
        return false;
      }

      var contentMimeType = contentType.Split(';')[0];

      return contentMimeType.Equals("application/json", StringComparison.InvariantCultureIgnoreCase) ||
             contentMimeType.Equals("text/json", StringComparison.InvariantCultureIgnoreCase) ||
             (contentMimeType.StartsWith("application/vnd", StringComparison.InvariantCultureIgnoreCase) &&
              contentMimeType.EndsWith("+json", StringComparison.InvariantCultureIgnoreCase));
    }
  }
}