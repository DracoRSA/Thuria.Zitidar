using System;

namespace Thuria.Zitidar.Nancy
{
  /// <summary>
  /// Bad Request Service Error exception
  /// </summary>
  public class BadRequestServiceErrorException : Exception, IThuriaHttpServiceError
  {
    /// <summary>
    /// Constructor
    /// </summary>
    public BadRequestServiceErrorException()
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message">Exception Message</param>
    public BadRequestServiceErrorException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message">Exception Message</param>
    /// <param name="innerException">Inner Exception</param>
    public BadRequestServiceErrorException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>
    /// Thuria Http Service Error
    /// </summary>
    public ThuriaHttpServiceError HttpServiceError => ThuriaHttpServiceErrorDefinition.BadRequestError(this);
  }
}
