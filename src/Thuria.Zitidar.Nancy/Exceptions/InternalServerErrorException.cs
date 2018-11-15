using System;

namespace Thuria.Zitidar.Nancy
{
  /// <summary>
  /// Internal Server Error Exception
  /// </summary>
  public class InternalServerErrorException : Exception, IThuriaHttpServiceError
  {
    /// <summary>
    /// Constructor
    /// </summary>
    public InternalServerErrorException()
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message">Exception Message</param>
    public InternalServerErrorException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message">Exception Message</param>
    /// <param name="innerException">Inner Exception</param>
    public InternalServerErrorException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>
    /// Thuria Http Service Error
    /// </summary>
    public ThuriaHttpServiceError HttpServiceError => ThuriaHttpServiceErrorDefinition.BadRequestError(this);
  }
}
