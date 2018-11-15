using System;
using Nancy;

namespace Thuria.Zitidar.Nancy
{
  /// <summary>
  /// Thuria Http Service Error Definition
  /// </summary>
  public static class ThuriaHttpServiceErrorDefinition
  {
    /// <summary>
    /// Not Found Error
    /// </summary>
    /// <param name="runtimeException">Runtime Exception</param>
    /// <returns>A Thuria Http Service Error model</returns>
    public static ThuriaHttpServiceError NotFoundError(Exception runtimeException)
    {
      return new ThuriaHttpServiceError
               {
                 HttpStatusCode          = HttpStatusCode.NotFound,
                 ServiceErrorModel = new ThuriaServiceErrorModel
                                             {
                                               Details    = $"The requested entity was not found. {runtimeException.Message}",
                                               StackTrace = runtimeException.ToString()
                                             }
               };
    }

    /// <summary>
    /// Bad Request Error
    /// </summary>
    /// <param name="runtimeException">Runtime Exception</param>
    /// <returns>A Thuria Http Service Error model</returns>
    public static ThuriaHttpServiceError BadRequestError(Exception runtimeException)
    {
      return new ThuriaHttpServiceError
               {
                 HttpStatusCode          = HttpStatusCode.BadRequest,
                 ServiceErrorModel = new ThuriaServiceErrorModel
                                             {
                                               Details    = $"An invalid service request was received. {runtimeException.Message}",
                                               StackTrace = runtimeException.ToString()
                                             }
               };
    }

    /// <summary>
    /// General Error
    /// </summary>
    /// <param name="runtimeException">Runtime Exception</param>
    /// <returns>A Thuria Http Service Error model</returns>
    public static ThuriaHttpServiceError GeneralError(Exception runtimeException)
    {
      return new ThuriaHttpServiceError
               {
                 HttpStatusCode          = HttpStatusCode.BadRequest,
                 ServiceErrorModel = new ThuriaServiceErrorModel
                                             {
                                               Details    = $"An error occured during processing the request. {runtimeException.Message}",
                                               StackTrace = runtimeException.ToString()
                                             }
               };
    }

    /// <summary>
    /// Internal Server Error
    /// </summary>
    /// <param name="runtimeException">Runtime Exception</param>
    /// <returns>A Thuria Http Service Error model</returns>
    public static ThuriaHttpServiceError InternalServerError(Exception runtimeException)
    {
      return new ThuriaHttpServiceError
               {
                 HttpStatusCode          = HttpStatusCode.InternalServerError,
                 ServiceErrorModel = new ThuriaServiceErrorModel
                                             {
                                               Details    = $"There was an internal server error during processing the request. {runtimeException.Message}",
                                               StackTrace = runtimeException.ToString()
                                             }
               };
    }

    /// <summary>
    /// Bad Request Error
    /// </summary>
    /// <param name="runtimeException">Runtime Exception</param>
    /// <returns>A Thuria Http Service Error model</returns>
    public static ThuriaHttpServiceError InvalidTokenError(Exception runtimeException)
    {
      return new ThuriaHttpServiceError
               {
                 HttpStatusCode          = HttpStatusCode.BadRequest,
                 ServiceErrorModel = new ThuriaServiceErrorModel
                                             {
                                               Details    = $"Invalid API Token. {runtimeException.Message}",
                                               StackTrace = runtimeException.ToString()
                                             }
               };
    }
  }
}
