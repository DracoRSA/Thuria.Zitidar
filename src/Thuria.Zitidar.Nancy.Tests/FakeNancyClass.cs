namespace Thuria.Zitidar.Nancy.Tests
{
  public interface IFakePerRequestNancyClass
  {
  }

  public class FakePerRequestNancyClass : IFakePerRequestNancyClass
  {
  }
  public class FakePerRequestNancyClass2 : IFakePerRequestNancyClass
  {
  }

  public interface IFakeTransientNancyClass
  {
  }

  public class FakeTransientNancyClass : IFakeTransientNancyClass
  {
  }
  public class FakeTransientNancyClass2 : IFakeTransientNancyClass
  {
  }

  public interface IFakeSingletonNancyClass
  {
  }

  public class FakeSingletonNancyClass : IFakeSingletonNancyClass
  {
  }
  public class FakeSingletonNancyClass2 : IFakeSingletonNancyClass
  {
  }
}
