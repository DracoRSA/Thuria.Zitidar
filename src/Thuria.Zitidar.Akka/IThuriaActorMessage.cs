using System;

namespace Thuria.Zitidar.Akka
{
  /// <summary>
  /// Thuria Actor Message
  /// </summary>
  public interface IThuriaActorMessage
  {
    /// <summary>
    /// Message ID
    /// </summary>
    Guid Id { get; }
  }
}
