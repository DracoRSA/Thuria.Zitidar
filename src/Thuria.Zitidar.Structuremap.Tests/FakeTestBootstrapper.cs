namespace Thuria.Zitidar.Structuremap.Tests
{
  public class FakeTestBootstrapper : StructuremapThuriaBootstrapper
  {
    public bool IsScanningFiles => _scanFiles;

    public new static IStructuremapThuriaBootstrapper Create()
    {
      return new FakeTestBootstrapper();
    }
  }
}