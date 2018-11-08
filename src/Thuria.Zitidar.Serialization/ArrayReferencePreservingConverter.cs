using System;
using System.Collections;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Thuria.Zitidar.Serialization
{
  /// <summary>
  /// Array Reference Preserving Converter
  /// </summary>
  public class ArrayReferencePreservingConverter : JsonConverter
  {
    private const string RefProperty    = "$ref";
    private const string IdProperty     = "$id";
    private const string ValuesProperty = "$values";

    /// <summary>
    /// Can Convert indicator
    /// </summary>
    /// <param name="objectType">Object Type</param>
    /// <returns>Boolean indicating if this converter can handle the given type</returns>
    public override bool CanConvert(Type objectType)
    {
      return objectType.IsArray;
    }

    /// <summary>
    /// Can Write indicator 
    /// </summary>
    public override bool CanWrite => false;

    /// <summary>
    /// Read Json
    /// </summary>
    /// <param name="reader">Json Reader</param>
    /// <param name="objectType">Object Type</param>
    /// <param name="existingValue">Existing Value</param>
    /// <param name="serializer">Json Serializer</param>
    /// <returns></returns>
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      switch (reader.TokenType)
      {
        case JsonToken.Null:
          return null;

        case JsonToken.StartArray:
          {
            // No $ref.  Deserialize as a List<T> to avoid infinite recursion and return as an array.
            var elementType = objectType.GetElementType();
            var listType    = typeof(List<>).MakeGenericType(elementType);
            var list        = (IList)serializer.Deserialize(reader, listType);
            if (list == null)
            {
              return null;
            }

            var array = Array.CreateInstance(elementType, list.Count);
            list.CopyTo(array, 0);
            return array;
          }

        default:
          {
            var obj    = JObject.Load(reader);
            var refId  = (string)obj[RefProperty];
            if (refId != null)
            {
              var reference = serializer.ReferenceResolver.ResolveReference(serializer, refId);
              if (reference != null)
              {
                return reference;
              }
            }

            var values = obj[ValuesProperty];
            if (values == null || values.Type == JTokenType.Null)
            {
              return null;
            }

            if (!(values is JArray))
            {
              throw new JsonSerializationException(string.Format("{0} was not an array", values));
            }

            var count       = ((JArray)values).Count;
            var elementType = objectType.GetElementType();
            var array       = Array.CreateInstance(elementType, count);

            var objId = (string)obj[IdProperty];
            if (objId != null)
            {
              // Add the empty array into the reference table BEFORE poppulating it,
              // to handle recursive references.
              serializer.ReferenceResolver.AddReference(serializer, objId, array);
            }

            var listType = typeof(List<>).MakeGenericType(elementType);
            using (var subReader = values.CreateReader())
            {
              var list = (IList)serializer.Deserialize(subReader, listType);
              list.CopyTo(array, 0);
            }

            return array;
          }
      }
    }

    /// <summary>
    /// Write Json
    /// </summary>
    /// <param name="writer">Json Writer</param>
    /// <param name="value">Value</param>
    /// <param name="serializer">Json Serializer</param>
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      throw new NotImplementedException();
    }
  }
}
