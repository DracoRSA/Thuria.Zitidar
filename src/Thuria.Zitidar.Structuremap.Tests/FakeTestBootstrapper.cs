namespace Thuria.Zitidar.Structuremap.Tests
{
  public class FakeTestBootstrapper : StructuremapThuriaBootstrapper
  {
    public bool IsScanningFiles => this._scanFiles;

    public new static IStructuremapThuriaBootstrapper Create()
    {
      return new FakeTestBootstrapper();
    }
  }
}