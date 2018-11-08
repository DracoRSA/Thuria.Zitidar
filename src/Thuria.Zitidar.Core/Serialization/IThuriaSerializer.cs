namespace Thuria.Zitidar.Core
{
  /// <summary>
  /// Thuria Serializer
  /// </summary>
  public interface IThuriaSerializer
  {
    /// <summary>
    /// Deserialize an object
    /// </summary>
    /// <typeparam name="T">Object Type</typeparam>
    /// <param name="serializationString">Serialization String</param>
    /// <param name="throwException">Throw Exception on failure indicator (Default = false)</param>
    /// <returns>
    /// If serialization fails and throwException is false, a null is returned
    /// If serialization fails and throwException is true, an exception is thrown with the error details
    /// A newly created object of type with its data deserialized from the input string
    /// </returns>
    T DeserializeObject<T>(string serializationString, bool throwException = false);

    /// <summary>
    /// Deserialize an object
    /// </summary>
    /// <param name="serializationString">Serialization String</param>
    /// <param name="throwException">Throw Exception on failure indicator (Default = false)</param>
    /// <returns>
    /// If serialization fails and throwException is false, a null is returned
    /// If serialization fails and throwException is true, an exception is thrown with the error details
    /// A newly created object with its data deserialized from the input string
    /// </returns>
    object DeserializeObject(string serializationString, bool throwException = false);

    /// <summary>
    /// Serialize an object to a string
    /// </summary>
    /// <typeparam name="T">Object Type</typeparam>
    /// <param name="objectToSerialize">Object to serialize</param>
    /// <param name="serializerSettings">Serializer Settings (Optional)</param>
    /// <returns>String representation of the object</returns>
    string SerializeObject<T>(T objectToSerialize, IThuriaSerializerSettings serializerSettings = null);
  }
}
