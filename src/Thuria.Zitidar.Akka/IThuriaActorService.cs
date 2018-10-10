using System;
using Thuria.Zitidar.Core;

namespace Thuria.Zitidar.Akka
{
  /// <summary>
  /// Thuria Actor Service
  /// </summary>
  public interface IThuriaActorService : IThuriaStartable, IThuriaStoppable, IThuriaNamedInstance, IDisposable
  {
    /// <summary>
    /// Actor System
    /// </summary>
    IThuriaActorSystem ActorSystem { get; }
  }
}