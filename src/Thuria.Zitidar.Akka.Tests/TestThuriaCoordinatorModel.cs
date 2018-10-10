using Akka.Actor;

using NSubstitute;
using NUnit.Framework;
using FluentAssertions;

using Thuria.Zitidar.Akka.Models;

namespace Thuria.Zitidar.Akka.Tests
{
  [TestFixture]
  public class TestThuriaCoordinatorModel
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      var actorMessage = Substitute.For<IThuriaActorMessage>();
      var actorRef     = Substitute.For<IActorRef>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var coordinatorDataModel = new ThuriaCoordinatorDataModel(actorMessage, actorRef, actorRef);
      //---------------Test Result -----------------------
      coordinatorDataModel.Should().NotBeNull();
    }
  }
}
