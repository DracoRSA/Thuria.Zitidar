using System;

namespace Thuria.Zitidar.Nancy
{
  /// <summary>
  /// Thuria Service Error Model
  /// </summary>
  public class ThuriaServiceErrorModel
  {
    /// <summary>
    /// Error Time stamp
    /// </summary>
    public DateTime TimeStamp { get; } = DateTime.Now;

    /// <summary>
    /// Error details
    /// </summary>
    public string Details { get; set; }

    /// <summary>
    /// Error Stack Trace
    /// </summary>
    public string StackTrace { get; set; }
  }
}