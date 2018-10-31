using System;

namespace Thuria.Zitidar.Core
{
  /// <summary>
  /// Thuria Logger
  /// </summary>
  public interface IThuriaLogger
  {
    /// <summary>
    /// Log a message
    /// </summary>
    /// <param name="logSeverity">Log Severity</param>
    /// <param name="logMessage">Log message</param>
    /// <param name="exceptionMessage">Exception (optional)</param>
    void LogMessage(LogSeverity logSeverity, string logMessage, Exception exceptionMessage = null);
  }
}
