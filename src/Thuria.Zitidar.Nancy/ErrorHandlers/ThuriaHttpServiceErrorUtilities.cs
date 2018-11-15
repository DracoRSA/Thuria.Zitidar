using System;

namespace Thuria.Zitidar.Nancy
{
  /// <summary>
  /// Thuria Http Service Error utilities
  /// </summary>
  public static class ThuriaHttpServiceErrorUtilities
  {
    /// <summary>
    /// Extract From Exception
    /// </summary>
    /// <param name="exception">Exception</param>
    /// <param name="defaultValue">Default Value</param>
    /// <returns>Thuria Http Service Error</returns>
    public static ThuriaHttpServiceError ExtractFromException(Exception exception, ThuriaHttpServiceError defaultValue)
    {
      switch (exception)
      {
        case IThuriaHttpServiceError exceptionWithServiceError:
          return exceptionWithServiceError.HttpServiceError;

        default:
          return defaultValue;
      }
    }
  }
}
