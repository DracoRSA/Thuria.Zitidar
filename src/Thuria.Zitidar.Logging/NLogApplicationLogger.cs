using System;
using NLog;
using Thuria.Zitidar.Core;

namespace Thuria.Zitidar.Logging
{
  /// <summary>
  /// NLog Application Logger
  /// </summary>
  public class NLogApplicationLogger : Logger, IThuriaLogger
  {
    /// <summary>
    /// Log A Message
    /// </summary>
    /// <param name="logSeverity">Log Severity</param>
    /// <param name="logMessage">Log Message</param>
    /// <param name="exceptionMessage">Exception details</param>
    public void LogMessage(LogSeverity logSeverity, string logMessage, Exception exceptionMessage = null)
    {
      switch (logSeverity)
      {
        case LogSeverity.Exception:
          Fatal(exceptionMessage, logMessage);
          break;

        case LogSeverity.Error:
          Error(exceptionMessage, logMessage);
          break;

        case LogSeverity.Warning:
          Warn(logMessage);
          break;

        case LogSeverity.Info:
          Info(logMessage);
          break;

        default:
          Debug(logMessage);
          break;
      }
    }
  }
}
