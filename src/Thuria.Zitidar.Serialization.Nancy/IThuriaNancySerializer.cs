using Nancy;
using Nancy.ModelBinding;

using Thuria.Zitidar.Core;

namespace Thuria.Zitidar.Serialization.Nancy
{
  /// <summary>
  /// Thuria Nancy Serializer
  /// </summary>
  public interface IThuriaNancySerializer : IThuriaSerializer, ISerializer, IBodyDeserializer
  {
  }
}
