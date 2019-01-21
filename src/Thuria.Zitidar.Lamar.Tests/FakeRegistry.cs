using Lamar;

namespace Thuria.Zitidar.Lamar.Tests
{
  public class FakeRegistry : ServiceRegistry
  {
    public FakeRegistry()
    {
      For<IFakeInterface>().Use<FakeClass>();
    }
  }
}
