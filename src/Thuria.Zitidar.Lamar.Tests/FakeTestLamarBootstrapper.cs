namespace Thuria.Zitidar.Lamar.Tests
{
  public class FakeTestLamarBootstrapper : LamarThuriaBootstrapper
  {
    public bool IsScanningFiles => this._scanFiles;

    public new static ILamarThuriaBootstrapper Create => new FakeTestLamarBootstrapper();
  }
}
