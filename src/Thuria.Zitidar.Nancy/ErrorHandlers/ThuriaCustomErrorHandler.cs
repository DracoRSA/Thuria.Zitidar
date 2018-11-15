using System;

using Nancy;
using Nancy.Bootstrapper;
using Nancy.Responses.Negotiation;

namespace Thuria.Zitidar.Nancy
{
  /// <summary>
  /// Thuria Custom Error Handler
  /// </summary>
  public class ThuriaCustomErrorHandler
  {
    /// <summary>
    /// Enable the error handler on the specified pipeline
    /// </summary>
    /// <param name="pipelines"></param>
    /// <param name="responseNegotiator"></param>
    public static void Enable(IPipelines pipelines, IResponseNegotiator responseNegotiator)
    {
      if (pipelines == null) { throw new ArgumentNullException(nameof(pipelines)); }
      if (responseNegotiator == null) { throw new ArgumentNullException(nameof(responseNegotiator)); }

      pipelines.OnError += (context, exception) => HandleException(context, exception, responseNegotiator);
    }

    private static Response HandleException(NancyContext context, Exception exception, IResponseNegotiator responseNegotiator)
    {
      LogException(context, exception);
      return CreateNegotiatedResponse(context, responseNegotiator, exception);
    }

    private static void LogException(NancyContext context, Exception exception)
    {
    }

    private static Response CreateNegotiatedResponse(NancyContext context, IResponseNegotiator responseNegotiator, Exception exception)
    {
      var httpServiceError = ThuriaHttpServiceErrorUtilities.ExtractFromException(exception, ThuriaHttpServiceErrorDefinition.GeneralError(exception));

      var negotiator = new Negotiator(context)
                       .WithStatusCode(httpServiceError.HttpStatusCode)
                       .WithModel(httpServiceError.ServiceErrorModel);

      return responseNegotiator.NegotiateResponse(negotiator, context);
    }
  }
}