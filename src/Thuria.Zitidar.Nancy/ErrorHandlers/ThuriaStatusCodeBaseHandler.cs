using System;

using Nancy;
using Nancy.ErrorHandling;
using Nancy.Responses.Negotiation;

namespace Thuria.Zitidar.Nancy
{
  /// <summary>
  /// Thuria Status Code Base Handler
  /// </summary>
  public abstract class ThuriaStatusCodeBaseHandler : IStatusCodeHandler
  {
    private readonly IResponseNegotiator _responseNegotiator;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="responseNegotiator">Response Negotiator</param>
    protected ThuriaStatusCodeBaseHandler(IResponseNegotiator responseNegotiator)
    {
      _responseNegotiator = responseNegotiator ?? throw new ArgumentNullException(nameof(responseNegotiator));
    }

    /// <summary>
    /// Determines if the Status Code is handled by the Status Code Handler
    /// </summary>
    /// <param name="statusCode">Http Status Code</param>
    /// <param name="context">Nancy Context</param>
    /// <returns>A bool indicating if the Http Status Code is handled by the Status Code Handler</returns>
    public abstract bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context);

    /// <summary>
    /// Handle Implementation
    /// </summary>
    /// <param name="statusCode">Http Status Code</param>
    /// <param name="context">Nancy Context</param>
    public abstract void Handle(HttpStatusCode statusCode, NancyContext context);

    /// <summary>
    /// Retrieve Service Error Exception from the Nancy Context
    /// </summary>
    /// <param name="nancyContext">Nancy Context</param>
    /// <returns>The Exception in the Nancy Context</returns>
    protected Exception RetrieveServiceErrorException(NancyContext nancyContext)
    {
      Exception internalException = null;

      if (nancyContext.Items.ContainsKey("OnErrorContextHook"))
      {
        internalException = nancyContext.Items["OnErrorContextHook"] as Exception;
      }
      else if (nancyContext.Items.ContainsKey("ERROR_EXCEPTION"))
      {
        internalException = nancyContext.Items["ERROR_EXCEPTION"] as Exception;
      }

      return internalException;
    }

    /// <summary>
    /// Set Nancy Context Response
    /// </summary>
    /// <param name="nancyContext">Nancy Context</param>
    /// <param name="statusCode">Http Status Code</param>
    /// <param name="serviceError">Thuria Service Error Model</param>
    protected void SetContextResponse(NancyContext nancyContext, HttpStatusCode statusCode, ThuriaServiceErrorModel serviceError)
    {
      nancyContext.NegotiationContext = nancyContext.NegotiationContext = new NegotiationContext();
      var negotiator = new Negotiator(nancyContext)
                       .WithStatusCode(statusCode)
                       .WithModel(serviceError)
                       .WithContentType("application/json");

      nancyContext.Response = _responseNegotiator.NegotiateResponse(negotiator, nancyContext);
    }
  }
}
