using System;
using Akka.Actor;

namespace Thuria.Zitidar.Akka.Models
{
  /// <summary>
  /// Thuria Coordinator Data Model
  /// </summary>
  public class ThuriaCoordinatorDataModel
  {
    /// <summary>
    /// Thuria Coordinator Data Model constructor
    /// </summary>
    /// <param name="actorMessage">Actor Message</param>
    /// <param name="originalActor">Original Sending Actor</param>
    /// <param name="processingActor">Message Processing Actor</param>
    public ThuriaCoordinatorDataModel(IThuriaActorMessage actorMessage, IActorRef originalActor, IActorRef processingActor)
    {
      ActorMessage    = actorMessage ?? throw new ArgumentNullException(nameof(actorMessage));
      OriginalActor   = originalActor ?? throw new ArgumentNullException(nameof(originalActor));
      ProcessingActor = processingActor ?? throw new ArgumentNullException(nameof(processingActor));
    }

    /// <summary>
    /// Actor Message
    /// </summary>
    public IThuriaActorMessage ActorMessage { get; private set; }

    /// <summary>
    /// Original Sending Actor
    /// </summary>
    public IActorRef OriginalActor { get; private set; }

    /// <summary>
    /// Message Processing Actor
    /// </summary>
    public IActorRef ProcessingActor { get; private set; }
  }
}
