using StructureMap;

namespace Thuria.Zitidar.Structuremap.Tests
{
  public class FakeRegistry : Registry
  {
    public FakeRegistry()
    {
      this.For<IFakeInterface>().Use<FakeClass>();
    }
  }
}