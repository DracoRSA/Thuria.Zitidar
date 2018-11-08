using Newtonsoft.Json;
using System.Collections.Generic;

namespace Thuria.Zitidar.Serialization.Nancy.Tests
{
  public class FakeTestClass
  {
    public FakeTestClass()
    {
    }

    [JsonConstructor]
    public FakeTestClass(bool isTested, List<string> messages)
    {
      IsTested = isTested;
      Messages = messages ?? new List<string>();
    }

    public bool IsTested { get; set; }
    public IEnumerable<string> Messages { get; set; }
  }
}
