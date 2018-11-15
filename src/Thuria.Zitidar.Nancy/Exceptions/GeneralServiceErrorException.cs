using System;

namespace Thuria.Zitidar.Nancy
{
  /// <summary>
  /// General Service Error exception
  /// </summary>
  public class GeneralServiceErrorException : Exception, IThuriaHttpServiceError
  {
    /// <summary>
    /// Constructor
    /// </summary>
    public GeneralServiceErrorException()
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message">Exception Message</param>
    public GeneralServiceErrorException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message">Exception Message</param>
    /// <param name="innerException">Inner Exception</param>
    public GeneralServiceErrorException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>
    /// Thuria Http Service Error
    /// </summary>
    public ThuriaHttpServiceError HttpServiceError => ThuriaHttpServiceErrorDefinition.GeneralError(this);
  }
}
