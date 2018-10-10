using System;
using Akka.Actor;
using Thuria.Zitidar.Core;

namespace Thuria.Zitidar.Akka
{
  /// <summary>
  /// Thuria Actor System
  /// </summary>
  public interface IThuriaActorSystem : IThuriaStartable, IThuriaStoppable, IDisposable
  {
    /// <summary>
    /// Actor System
    /// </summary>
    ActorSystem ActorSystem { get; set; }
  }
}
