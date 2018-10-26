namespace Thuria.Zitidar.Core
{
  /// <summary>
  /// Logger Severity Enumerations
  /// </summary>
  public enum LogSeverity
  {
    /// <summary>
    /// Log all messages
    /// </summary>
    Debug,

    /// <summary>
    /// Log Info, Warning, Error and Exception messages
    /// </summary>
    Info,

    /// <summary>
    /// Log Warning, Error and Exception messages
    /// </summary>
    Warning,

    /// <summary>
    /// Log Error and Exception messages
    /// </summary>
    Error,

    /// <summary>
    /// Log Exception messages
    /// </summary>
    Exception
  }

  /// <summary>
  /// Thuria Settings Type
  /// </summary>
  public enum ThuriaSettingsType
  {
    /// <summary>
    /// JSON Settings File
    /// </summary>
    JsonFile,

    /// <summary>
    /// Environment Variables
    /// </summary>
    Environment,

    /// <summary>
    /// Command Line
    /// </summary>
    CommandLine
  }
}
